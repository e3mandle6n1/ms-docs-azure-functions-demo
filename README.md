# ms-docs-azure-functions-demo

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Azure Functions](https://img.shields.io/badge/Azure_Functions-v4-0062AD?style=flat-square&logo=microsoftazure&logoColor=white)
![C#](https://img.shields.io/badge/C%23-Isolated_Worker-239120?style=flat-square&logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-HTTP-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Azure Developer CLI](https://img.shields.io/badge/Azure_Developer_CLI-azd-0078D4?style=flat-square&logo=microsoftazure&logoColor=white)
![OpenTelemetry](https://img.shields.io/badge/OpenTelemetry-Enabled-000000?style=flat-square&logo=opentelemetry&logoColor=white)
![Application Insights](https://img.shields.io/badge/Application_Insights-Monitoring-68217A?style=flat-square&logo=microsoftazure&logoColor=white)
![Azure Storage](https://img.shields.io/badge/Azure_Storage-Managed_Identity-0078D4?style=flat-square&logo=microsoftazure&logoColor=white)
![Bicep](https://img.shields.io/badge/Bicep-IaC-0078D4?style=flat-square&logo=microsoftazure&logoColor=white)

Azure Functions (.NET 10 isolated worker) demo project deployed with [Azure Developer CLI](https://learn.microsoft.com/azure/developer/azure-developer-cli/) (`azd`).

## Run locally

```bash
dotnet run
```

The host prints the local URL when it starts (default port **7137** from `Properties/launchSettings.json`). All HTTP routes are prefixed with `/api`.

## GreetUser

`GET /api/greet` — returns a JSON greeting for the given `name` query parameter.

| Query param | Required | Description |
|-------------|----------|-------------|
| `name`      | Yes      | Name to greet |
| `lang`      | No       | Language code (`en`, `fr`, `es`, `de`). Defaults to English. |

### Examples

```bash
# Basic greeting
curl "http://localhost:7137/api/greet?name=Emandleni"
# {"message":"Hello, Emandleni!"}

# French greeting
curl "http://localhost:7137/api/greet?name=Emandleni&lang=fr"
# {"message":"Bonjour, Emandleni!"}

# Missing name (400 Bad Request)
curl "http://localhost:7137/api/greet"
# {"error":"Query parameter 'name' is required."}
```

## CreateTodo

`POST /api/todos` — creates a todo from a JSON body and returns the created item.

| Field   | Required | Description |
|---------|----------|-------------|
| `title` | Yes      | Todo title (max 200 characters) |

### Examples

```bash
# Create a todo (201 Created)
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d '{"title":"Learn Azure Functions"}'
# {"id":"...","title":"Learn Azure Functions","createdAt":"..."}

# Missing title (400 Bad Request)
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d '{}'
# {"error":"Field 'title' is required."}

# Invalid JSON (400 Bad Request)
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d 'not json'
# {"error":"Request body must be valid JSON."}
```

### Deploy and test in Azure

```bash
azd deploy
```

After deploy, replace `localhost:7137` with your Function App URL from `azd show`.

## Project structure

```
ms-docs-azure-functions-demo/
├── Program.cs
├── functions/
│   ├── CreateTodo.cs
│   ├── Greetuser.cs
│   └── HttpExample.cs
├── models/
│   └── Todo.cs
├── Properties/
│   └── launchSettings.json
├── infra/
│   ├── main.bicep
│   ├── main.parameters.json
│   ├── abbreviations.json
│   └── app/
│       ├── api.bicep
│       ├── rbac.bicep
│       ├── storage-PrivateEndpoint.bicep
│       └── vnet.bicep
├── .github/
│   └── workflows/
│       └── azure-dev.yml
├── azure.yaml
├── host.json
├── local.settings.json
└── ms-docs-azure-functions-demo.csproj
```
