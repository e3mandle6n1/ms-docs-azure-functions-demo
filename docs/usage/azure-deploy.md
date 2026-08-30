# Azure Deploy

Infrastructure provisions a Flex Consumption Function App, storage, managed identity, and related settings via Bicep. Application code is published with `azd deploy` (zip / package), not the local Docker image.

## 1. Authenticate and select environment

```bash
azd auth login
azd env select <environment-name>   # or azd init if first time
```

Confirm subscription and location match the intended environment before provisioning.

## 2. Provision and deploy

```bash
azd up
```

Or separately:

```bash
azd provision
azd deploy
```

After deploy, get the Function App hostname:

```bash
azd show
```

Swagger UI: `https://<function-app-name>.azurewebsites.net/api/swagger`.

## Optional app settings

Weather (falls back to `FakeWeatherService` without a key):

```bash
az functionapp config appsettings set \
  --name <function-app-name> \
  --resource-group <resource-group-name> \
  --settings "WeatherApi__ApiKey=<your-key>"
```

## Test after deploy

Replace `localhost:<port>` in the [function examples](../api/functions.md) with your Function App base URL.

## View in the Azure Portal

| Resource | Portal path |
|----------|-------------|
| Function App | **Function App** in the deployment resource group |
| Storage | Storage account linked via `AzureWebJobsStorage__*ServiceUri` |
| Logs | Application Insights → **Logs** (see [Monitoring](monitoring.md)) |

Next: [GitHub Actions](github-actions.md) · [Troubleshooting](troubleshooting.md).
