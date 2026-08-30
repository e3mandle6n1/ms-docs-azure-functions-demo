# Troubleshooting

## Clean slate checklist

When a build, `dotnet run`, or `azd deploy` feels stuck:

```bash
# Stop leftover local Functions hosts and builds
pkill -f 'dotnet run' 2>/dev/null
pkill -f 'func host' 2>/dev/null
pkill -f 'dotnet publish' 2>/dev/null

# Release MSBuild / Roslyn compiler locks from interrupted builds
dotnet build-server shutdown

# Confirm default Functions ports are free (7071, 7137)
lsof -i :7071 -i :7137

# List any remaining deploy/build processes
pgrep -fl 'azd|func |dotnet|MSBuild|VBCSCompiler'

# Clean artifacts and verify Release build completes
rm -rf bin obj && dotnet build -c Release

# Redeploy to Azure
azd deploy
```

Run the `pkill` / `build-server shutdown` lines after any interrupted build or deploy. If `pgrep` or `lsof` still show processes or ports in use, kill those PIDs before retrying.

## `azd deploy` hangs at "Packaging (Publishing .NET project)"

**Symptoms**

- `azd deploy` sits on `api: Packaging (Publishing .NET project)` indefinitely.
- Azure resources look provisioned, but no functions appear in the Function App.
- A plain `dotnet build` also hangs (stuck on `CoreCompile`).

**Cause**

Packaging is local `dotnet publish -c Release`. Leftover processes from an interrupted `dotnet run` / `publish` / `azd deploy` hold Roslyn (`VBCSCompiler`) and MSBuild locks, so the next build deadlocks. Nothing reaches Azure until publish succeeds.

**Fix**

1. Cancel the hung deploy with `Ctrl+C`.
2. Run the [clean slate checklist](#clean-slate-checklist).
3. Re-run:

```bash
azd deploy
```

Add `--debug` if you want to see what azd is doing behind the spinner.

**Prevention**

- Do not run two builds/deploys of this project at the same time.
- After interrupting `dotnet run` / `azd deploy`, run `dotnet build-server shutdown` before the next attempt.
- If a function is missing after deploy, check `git status` first.

## Missing storage / 503 on `/api/health`

**Symptoms**

- Host fails at startup: `AzureWebJobsStorage:BlobServiceUri is required…`
- Or `/api/health` returns `503` with `"storage":` not `ok`.

**Cause**

Blob service URI missing or the process identity cannot reach the storage account.

**Fix**

Confirm `AzureWebJobsStorage__blobServiceUri` (and queue URI if needed) in `local.settings.json` or app settings, and that your login / managed identity has blob (and queue) data-plane roles on the account.
