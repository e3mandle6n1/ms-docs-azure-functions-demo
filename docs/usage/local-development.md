# Local Development

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Azure CLI login with access to the storage account used in `local.settings.json`
- Roles on that account: **Storage Blob Data Contributor**, **Storage Queue Data Contributor**
- Optional: Docker (for container practice), [WeatherAPI.com](https://www.weatherapi.com/) key

## Run with .NET

```bash
dotnet run
```

When the host starts, check the terminal — it lists every function URL and the listen port (often **7071**, not necessarily the value in `launchSettings.json`). All HTTP routes are prefixed with `/api`.

## Swagger UI

Swagger UI is at `/api/swagger` (wired in `Program.cs` via AzureFunctions.Worker.Extensions.Swashbuckle).

| Environment | URL |
|-------------|-----|
| Local       | `http://localhost:<port>/api/swagger` |
| Azure       | `https://<function-app-name>.azurewebsites.net/api/swagger` |

1. Start the host with `dotnet run` and leave that terminal visible — invocations and errors are logged there.
2. Open `/api/swagger` in your browser.
3. Expand an operation, fill parameters or body (use **Choose File** for multipart uploads), then **Execute**.
4. Confirm the run in the `dotnet run` terminal.

Timer and queue triggers do not appear in Swagger. `HttpExample` is hidden from the docs (`[ApiExplorerSettings(IgnoreApi = true)]`) but still works at `/api/HttpExample`. OpenAPI JSON: `/api/swagger/v1/swagger.json`.

Contract details: [Functions](../api/functions.md).

## Run in Docker

Build and run (host 8080 → container 80):

```bash
docker build -t functions-demo .
docker run --rm --name functions-demo -p 8080:80 functions-demo
```

Endpoints: `http://localhost:8080/api/...`. HTTP-only paths work out of the box; storage-backed functions need storage configuration, e.g. Azurite:

```bash
docker run --rm --name functions-demo -p 8080:80 \
  -e AzureWebJobsStorage="<connection-string>" \
  functions-demo
```

Stop with `Ctrl+C`, or `docker stop functions-demo`. `--rm` removes the container when it exits.

### Push to Azure Container Registry

```bash
az acr login --name <registry-name>
docker tag functions-demo <registry-url>/functions-demo:latest
docker push <registry-url>/functions-demo:latest
```

Or build on ACR (natively amd64):

```bash
az acr build --registry <registry-name> --image functions-demo:latest .
```

## Notes

- The Dockerfile runtime stage is pinned to `linux/amd64` (Functions base image has no arm64 variant). On Apple Silicon it runs under Rosetta; a platform-mismatch warning is harmless.
- The container is for local / registry practice. `azd deploy` targets **Flex Consumption**, which uses package deployment and does not run this custom image. To host the image you would need Dedicated/Elastic Premium Functions or Container Apps.

Next: [Azure deploy](azure-deploy.md).
