variable "namespace" {
  description = "Namespace de Kubernetes"
  type        = string
  default     = "locatic"
}

variable "image_repository" {
  type    = string
  default = "tiraprong/locationvoiture"
}

variable "image_tag" {
  description = "Tag de l'image Docker"
  type        = string
  default     = "latest"
}

variable "sqlite_storage_size" {
  description = "Taille du volume persistant Sqlite"
  type        = string
  default     = "500Mi"
}

variable "kube_config_path" {
  description = "Path de la config kubernetes"
  type        = string
  default     = "~/.kube/config"
}

variable "kube_context" {
  description = "Contexte kube ctl à utiliser"
  type        = string
  default     = "minikube"
}

variable "app_name" {
  type    = string
  default = "locatic-app"
}

variable "app_port" {
  type    = number
  default = 8080
}

variable "app_replicas" {
  type    = number
  default = 1
}

variable "node_env" {
  type    = string
  default = "production"
}

variable "app_log_level" {
  type    = string
  default = "info"
}
