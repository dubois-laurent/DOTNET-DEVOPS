# Location Voiture

Application ASP.NET Core MVC de location de voitures, construite autour de SQLite et accompagnée d'une chaîne DevOps locale pour la CI, le déploiement sur minikube et le monitoring.

## Vue d'ensemble

Ce dépôt contient :

- l'application web ASP.NET Core dans `app/`
- les tests automatisés dans `app/tests/LocationVoiture.Tests/`
- le pipeline GitHub Actions dans `.github/workflows/`
- l'infrastructure locale Terraform et Ansible dans `infra/`
- la stack Prometheus, Grafana et AlertManager dans `monitoring/`
- la documentation du mini-projet dans `docs/`

## Fonctionnalités

- Gestion des voitures, marques, modèles, clients et réservations
- Persistance SQLite
- Endpoint de santé sur `/health`
- Endpoint de métriques Prometheus sur `/metrics`
- Image Docker construisible localement et en CI
- Monitoring local avec Prometheus, Grafana et AlertManager
- Infrastructure locale pour minikube avec Terraform et Ansible

## Prérequis

- .NET 8 SDK
- Docker et Docker Compose
- Terraform 1.5 ou plus pour la partie infrastructure locale
- Ansible pour l'orchestration locale
- kubectl et minikube pour le déploiement Kubernetes local

## Lancer l'application en local

```bash
cd app
dotnet restore
dotnet run
```

Ensuite, ouvrir l'application depuis l'URL affichée par `dotnet run`.

## Lancer les tests

```bash
dotnet test app/tests/LocationVoiture.Tests/LocationVoiture.Tests.csproj --configuration Release
```

## Construire et lancer l'image Docker

Le Dockerfile de l'application se trouve dans `app/Dockerfile`.

```bash
docker build -t locationvoiture:latest -f app/Dockerfile app
docker run --rm -p 3000:8080 locationvoiture:latest
```

L'application Docker écoute sur le port `8080` dans le conteneur

## Monitoring local

La stack de monitoring se trouve dans `monitoring/`.

```bash
cd monitoring
copy .env.example .env
docker compose up -d
```

Accès local :

- Prometheus: http://localhost:9090
- Grafana: http://localhost:3001
- AlertManager: http://localhost:9093
- Application: http://localhost:3000

Le dashboard Grafana `DevOps App Overview` est provisionné automatiquement et Prometheus scrape l'application sur `http://app:8080/metrics`.

## Infra locale et minikube

Le dossier `infra/` contient la préparation locale du déploiement Kubernetes.

### Terraform

```bash
cd infra/terraform
terraform init
terraform plan
terraform apply
```

Terraform prépare notamment le namespace Kubernetes, le PVC SQLite, la configuration de l'application et le reverse proxy Nginx.

### Ansible

```bash
cd infra/ansible
ansible-playbook site.yml
```

Le playbook sert d'orchestrateur local entre Terraform, minikube et les ressources Kubernetes.

## CI GitHub Actions

Le workflow `.github/workflows/dotnet-ci.yml` exécute :

- les tests unitaires
- un contrôle de formatage via `dotnet format`
- la construction de l'image Docker
- un scan de sécurité avec Trivy
- la publication de l'image dans GHCR sur `main`

## Endpoints utiles

- `/` : interface MVC de l'application
- `/health` : vérification de santé
- `/metrics` : métriques Prometheus

## Structure rapide

- `app/` : code applicatif ASP.NET Core et Dockerfile
- `app/tests/LocationVoiture.Tests/` : tests xUnit
- `infra/terraform/` : préparation du namespace, du PVC SQLite, de l'app et de Nginx
- `infra/ansible/` : orchestration locale du déploiement
- `monitoring/` : Prometheus, Grafana, AlertManager et dashboards
- `docs/` : documentation du mini-projet