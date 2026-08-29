# Functions

All HTTP routes are under `/api`. Local examples use `localhost:7137` — replace with the port from `dotnet run` (or your Function App hostname after deploy).

## HealthCheck — `GET /api/health`

Checks blob storage connectivity. Returns `200` when healthy, `503` when storage is unreachable.

```bash
curl -i "http://localhost:7137/api/health"
```

## GreetUser — `GET /api/greet`

Returns a JSON greeting for the given `name` query parameter.

| Query param | Required | Description |
|-------------|----------|-------------|
| `name`      | Yes      | Name to greet |
| `lang`      | No       | Language (`en`, `fr`, `es`, `de`). Defaults to `en`. |

```bash
curl "http://localhost:7137/api/greet?name=Emandleni"
curl "http://localhost:7137/api/greet?name=Emandleni&lang=fr"
```

## CreateTodo — `POST /api/todos`

Creates one or more todos from a JSON array and persists them in blob storage.

| Field (per item) | Required | Description |
|------------------|----------|-------------|
| `title`          | Yes      | Todo title (max 200 characters) |

```bash
curl -i -X POST "http://localhost:7137/api/todos" \
  -H "Content-Type: application/json" \
  -d '[{"title":"Learn Azure Functions"}]'
```

## GetTodos — `GET /api/todos`

Returns all todos from blob storage.

```bash
curl -i "http://localhost:7137/api/todos"
```

## EchoHeaders — `GET /api/echo-headers`

Returns request headers as a JSON map (sorted). Useful for seeing proxy headers in Azure.

| Query param | Required | Description |
|-------------|----------|-------------|
| `filter`    | No       | Set to `interesting` for a subset of notable headers. Defaults to all. |

```bash
curl "http://localhost:7137/api/echo-headers"
curl "http://localhost:7137/api/echo-headers?filter=interesting"
curl -H "X-Demo: hello" "http://localhost:7137/api/echo-headers"
```

## GetWeather — `GET /api/weather/{city}`

Weather report via WeatherAPI.com when `WeatherApi__ApiKey` is set; otherwise `FakeWeatherService`.

| Route param | Required | Description |
|-------------|----------|-------------|
| `city`      | Yes      | City name (e.g. `Paris,FR` for disambiguation) |

```bash
curl -i "http://localhost:7137/api/weather/johannesburg"
curl -s "http://localhost:7137/api/weather/Paris,FR"
```

Local key (gitignored):

```json
"WeatherApi__ApiKey": "your-key-here"
```

Or: `dotnet user-secrets set "WeatherApi:ApiKey" "your-key-here"`.

## SaveToBlob — `POST /api/save`

Multipart file upload to the `uploads` container via managed identity (`IBlobUploadService`). Max 8 MB.

| Form field | Required | Description |
|------------|----------|-------------|
| `file`     | Yes      | File to store |

```bash
curl -i -X POST "http://localhost:7137/api/save" -F "file=@notes.txt"
```

## UploadAndProcess — `POST /api/upload`

Same upload as `SaveToBlob`, then enqueues a job on `demo-queue` (queue output binding). Returns `202 Accepted`; `ProcessQueueMessage` handles the job.

| Form field | Required | Description |
|------------|----------|-------------|
| `file`     | Yes      | File to store (max 8 MB) |

```bash
curl -i -X POST "http://localhost:7137/api/upload" -F "file=@report.txt"
```

## EnqueueMessage — `POST /api/messages`

Enqueues a JSON message onto the `demo-messages` storage queue (output binding). Returns `202 Accepted`.

| Body field | Required | Description |
|------------|----------|-------------|
| `message`  | Yes      | Text payload (max 500 characters) |

```bash
curl -i -X POST "http://localhost:7137/api/messages" \
  -H "Content-Type: application/json" \
  -d '{"message":"hello from the API"}'
```

## ProcessQueueMessage — queue trigger on `demo-queue`

Triggered when a message lands on `demo-queue`. Logs upload jobs (from `UploadAndProcess`) or raw message bodies. No HTTP endpoint.

```bash
az storage message put \
  --queue-name demo-queue \
  --content "hello from the queue" \
  --account-name <storage-account-name> \
  --auth-mode login

func azure functionapp logstream <function-app-name>
```

Locally, `dotnet run` uses the same storage account via `local.settings.json`, so enqueued messages trigger on your machine when your CLI identity has **Storage Queue Data Contributor**.

## Heartbeat — timer (every minute)

Logs a heartbeat line on a one-minute schedule. No HTTP endpoint. See [Monitoring](../usage/monitoring.md) for KQL.

## CleanupOldBlobs — timer (Mondays 04:00 UTC)

Deletes blobs in `uploads` last modified more than **2 days** ago. Failed deletes are logged and skipped.

To exercise locally without waiting for the schedule, temporarily change the NCRONTAB in `functions/CleanupOldBlobs.cs` and run `dotnet run`.

## HttpExample — `GET|POST /api/HttpExample`

Template sample endpoint; hidden from Swagger but still callable.
