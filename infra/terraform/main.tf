terraform {
  required_version = ">= 1.5"


  required_providers {
    kubernetes = {
      source  = "hashicorp/kubernetes"
      version = "~> 2.35"
    }
  }
}

provider "kubernetes" {
  config_path    = pathexpand(var.kube_config_path)
  config_context = var.kube_context
}

locals {
  labels = {
    app          = var.app_name
    "managed_by" = "terraform"
  }
}

resource "kubernetes_namespace" "locatic" {
  metadata {
    name = var.namespace
    labels = {
      project      = "locatic-app"
      "managed_by" = "terraform"
    }
  }
}

resource "kubernetes_config_map" "app" {
  metadata {
    name      = "app-config"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  data = {
    NODE_ENV      = var.node_env
    APP_PORT      = tostring(var.app_port)
    APP_LOG_LEVEL = var.app_log_level
    LOG_LEVEL     = var.app_log_level
    "ConnectionStrings__DefaultConnection" = "Data Source=/data/locationvoiture.db"
  }
}

resource "kubernetes_persistent_volume_claim" "sqlite" {
  wait_until_bound = false

  metadata {
    name      = "sqlite-persistent-volume-claim"
    namespace = kubernetes_namespace.locatic.metadata[0].name
    labels = {
      app = var.app_name
    }
  }

  spec {
    access_modes = ["ReadWriteOnce"]
    resources {
      requests = {
        storage = var.sqlite_storage_size
      }
    }
  }
}

resource "kubernetes_deployment" "app" {
  metadata {
    name      = var.app_name
    namespace = kubernetes_namespace.locatic.metadata[0].name
    labels    = local.labels
  }

  spec {
    replicas = var.app_replicas

    selector {
      match_labels = {
        app = var.app_name
      }
    }

    # Recreate strategy: with a single ReadWriteOnce SQLite volume, a rolling
    # update would deadlock (new pod can't mount the PVC while the old pod
    # still holds it).
    strategy {
      type = "Recreate"
    }

    template {
      metadata {
        labels = local.labels
      }

      spec {
        container {
          name              = "app"
          image             = "${var.image_repository}:${var.image_tag}"
          image_pull_policy = "IfNotPresent"

          port {
            name           = "http"
            container_port = var.app_port
          }

          env_from {
            config_map_ref {
              name = kubernetes_config_map.app.metadata[0].name
            }
          }

          volume_mount {
            name       = "sqlite-storage"
            mount_path = "/data"
          }

          readiness_probe {
            http_get {
              path = "/health"
              port = var.app_port
            }
            initial_delay_seconds = 5
            period_seconds        = 10
          }

          liveness_probe {
            http_get {
              path = "/health"
              port = var.app_port
            }
            initial_delay_seconds = 15
            period_seconds        = 20
          }
        }

        volume {
          name = "sqlite-storage"
          persistent_volume_claim {
            claim_name = kubernetes_persistent_volume_claim.sqlite.metadata[0].name
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "app" {
  metadata {
    name      = "${var.app_name}-svc"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  spec {
    selector = {
      app = var.app_name
    }

    port {
      name        = "http"
      port        = 80
      target_port = "http"
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_config_map" "nginx" {
  metadata {
    name      = "nginx-config"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  data = {
    "default.conf" = <<-EOT
      server {
        listen 80;
        server_name _;

        location / {
          proxy_pass         http://${kubernetes_service.app.metadata[0].name}:80;
          proxy_set_header   Host $host;
          proxy_set_header   X-Real-IP $remote_addr;
          proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        }
      }
    EOT
  }
}


resource "kubernetes_deployment" "nginx" {
  metadata {
    name      = "nginx"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = { app = "nginx" }
    }

    template {
      metadata {
        labels = { app = "nginx" }
      }

      spec {
        container {
          name  = "nginx"
          image = "nginx:1.27-alpine"

          port {
            container_port = 80
          }

          volume_mount {
            name       = "nginx-config"
            mount_path = "/etc/nginx/conf.d"   # nginx charge tout ce qui est dans ce dossier
          }
        }

        volume {
          name = "nginx-config"
          config_map {
            name = kubernetes_config_map.nginx.metadata[0].name
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "nginx" {
  metadata {
    name      = "nginx-svc"
    namespace = kubernetes_namespace.locatic.metadata[0].name
  }

  spec {
    selector = { app = "nginx" }

    port {
      port        = 80
      target_port = 80
      node_port   = 30080   # port fixe accessible via $(minikube ip):30080
    }

    type = "NodePort"
  }
}