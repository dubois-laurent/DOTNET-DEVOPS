variable "namespace" {
    description = "Namespace de Kubernetes"
    type = string
    default = "locatic"
}

variable "image_repository" {
  type    = string
  default = "ghcr.io/dubois-laurent/locationvoiture"
}

variable "image_tag" {
    description = "Tag de l'image Docker"
    type = string
    default = "1.0.0"
}

variable "sqlite_host_path" {
    description = "Path minikube Sqlite"
    type = string
    default = "/mnt/data/locatic"
}

variable "sqlite_storage_size" {
    description = "Taille du volume persistant Sqlite"
    type = string
    default = "500Mi"
}

variable "kube_config_path" {
    description = "Path de la config kubernetes"
    type = string
    default = "~/kube/config"
}

variable "kube_context" { 
    description = "Contexte kube ctl à utiliser"
    type = string
    default = "kind_devops_training"
}

variable "app_name" {
    type = string
    default = "locatic-app"
}

variable "app_port" {
    type = number
    default = 3000
}

variable "app_replicas" {
    type = number
    default = 3
}

variable "node_env" {
    type = string
    default = "production"
}

variable "app_log_level" {
  type    = string
  default = "info"
}

variable "db_name" {
  type    = string
  default = "appdb"
}

variable "db_user" {
  type    = string
  default = "appuser"
}

variable "db_password" {
  type      = string
  sensitive = true
}

variable "postgres_storage" {
  type    = string
  default = "1Gi"
}