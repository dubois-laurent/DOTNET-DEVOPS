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