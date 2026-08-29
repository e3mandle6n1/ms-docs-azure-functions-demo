# ms-docs-azure-functions-demo

[![Deploy Azure Functions](https://github.com/e3mandle6n1/ms-docs-azure-functions-demo/actions/workflows/azure-dev.yml/badge.svg)](https://github.com/e3mandle6n1/ms-docs-azure-functions-demo/actions/workflows/azure-dev.yml)

[![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff)](docs/usage/local-development.md)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![Azure Functions](https://img.shields.io/badge/Azure_Functions-v4-0062AD?logo=microsoftazure&logoColor=white)
![C#](https://img.shields.io/badge/C%23-Isolated_Worker-239120?logo=dotnet&logoColor=white)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=black)](docs/usage/local-development.md#swagger-ui)
![Azure Developer CLI](https://img.shields.io/badge/Azure_Developer_CLI-azd-0078D4?logo=microsoftazure&logoColor=white)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-Enabled-000000?logo=opentelemetry&logoColor=white)
![Application Insights](https://img.shields.io/badge/Application_Insights-Monitoring-68217A?logo=microsoftazure&logoColor=white)
![Azure Storage](https://img.shields.io/badge/Azure_Storage-Managed_Identity-0078D4?logo=microsoftazure&logoColor=white)
![Bicep](https://img.shields.io/badge/Bicep-IaC-0078D4?logo=microsoftazure&logoColor=white)
![KQL](https://img.shields.io/badge/KQL-Log_Queries-0078D4)

Azure Functions (.NET 10 isolated worker) demo for HTTP, timer, queue, and blob patterns. Deployed with [Azure Developer CLI](https://learn.microsoft.com/azure/developer/azure-developer-cli/) (`azd`) to a Flex Consumption Function App, with OpenTelemetry into Application Insights and storage access via managed identity.

## Responsibilities

- Expose HTTP APIs for greetings, todos, headers, weather, blob upload, and async upload jobs.
- Process storage queue messages and run scheduled blob cleanup / heartbeat timers.
- Persist todos and uploads in Azure Blob Storage using managed identity.
- Provision Function App, storage, identity, and optional VNet with `azd` + Bicep.
- Deploy on push to `master` through GitHub Actions.

## Architecture

```text
Client / Swagger -> HTTP functions (/api/...) -> services / repositories
                         |
              Timer / Queue triggers -> Blob Storage / demo-queue
                         |
              Flex Consumption Function App <- azd + Bicep (infra/)
                         |
              OpenTelemetry -> Application Insights
```

- App owns functions, models, services, and Swagger wiring (`functions/`, `services/`, `Program.cs`).
- Infra owns the Function App, storage, RBAC, and optional networking (`infra/`).
- CI provisions and deploys with `azd` (`.github/workflows/azure-dev.yml`).

More detail:

- [Local development](docs/usage/local-development.md)
- [Azure deploy](docs/usage/azure-deploy.md)
- [GitHub Actions](docs/usage/github-actions.md)
- [Monitoring (KQL)](docs/usage/monitoring.md)
- [Troubleshooting](docs/usage/troubleshooting.md)

## API

- [Functions](docs/api/functions.md) — HTTP routes, timers, and queue triggers

## Configuration

Required for storage-backed features (typically via `local.settings.json` locally, app settings in Azure):

```bash
# Blob / queue URIs for the Functions host (managed identity)
AzureWebJobsStorage__blobServiceUri=https://<storage-account>.blob.core.windows.net/
AzureWebJobsStorage__queueServiceUri=https://<storage-account>.queue.core.windows.net/
AzureWebJobsStorage__clientId=<user-assigned-mi-client-id>   # optional when using UAMI

# Optional — real weather data; without it FakeWeatherService is used
WeatherApi__ApiKey=<weatherapi-key>
```

Do not commit real credentials or API keys. Keep secrets in `local.settings.json` (gitignored), user secrets, or Azure app settings.

## Local Development

See [local development](docs/usage/local-development.md) for `dotnet run`, Swagger UI, and Docker.
