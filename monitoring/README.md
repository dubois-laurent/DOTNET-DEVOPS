# Monitoring

Stack locale Prometheus + Grafana + AlertManager pour superviser l'application ASP.NET Core du projet.

## Démarrage

```bash
cd monitoring
copy .env.example .env
# Ajuste si besoin les identifiants Grafana dans .env
docker compose up -d
```

## Accès

- Prometheus: http://localhost:9090
- Grafana: http://localhost:3001
- AlertManager: http://localhost:9093
- App: http://localhost:3000

## Points utiles

- La cible `app` scrape `http://app:8080/metrics`.
- Le dashboard `DevOps App Overview` est provisionné automatiquement.
- Les alertes sont envoyées au service `webhook-mock`.
