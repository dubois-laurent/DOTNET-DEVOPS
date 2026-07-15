variable "namespace" {
    description = "Namespace de Kubernetes"
    type = string
    default = "locatic"
}

variable "docker_image_name" {
    description = "Nom de l'image Docker"
    type = string
    default = "ghcr.io/dubois-laurent/locationvoiture"
}

variable "image_tag" {
    description = "Tag de l'image Docker"
    type = string
    default = "latest"
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