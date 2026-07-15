terraform {
    required_providers { 
        kubernetes = { 
            source = "hashicorp/kubernetes"
            version = "~> 2.27"
        }
    }
    required_version = ">= 1.6.0"
}

provider "kubernetes"{
    config_path = "~/.kube/config"
    config_context = "minikudbe"
}

resource "kubernetes_namespace" "locatic" { 
    metadata {
        name = var.namespace
        labels = {
            app = "locatic"
            manged-by = "terraform"
        }
    }
}