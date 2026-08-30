# GitHub Actions

CI/CD is defined in `.github/workflows/azure-dev.yml`. On push to `master` (or `workflow_dispatch`), the workflow:

1. Logs in to Azure via OIDC
2. Runs `azd provision --no-prompt`
3. Runs `azd deploy --no-prompt`

## Required repository secrets

| Secret | Description |
|--------|-------------|
| `AZURE_CLIENT_ID` | App registration used for GitHub OIDC |
| `AZURE_TENANT_ID` | Azure AD tenant ID |
| `AZURE_SUBSCRIPTION_ID` | Target subscription |

Workflow env defaults (in the YAML): `AZURE_ENV_NAME`, `AZURE_LOCATION`, `VNET_ENABLED`, `ALLOW_USER_IDENTITY_PRINCIPAL`.

## Local parity

To mirror CI from your machine after `az login`:

```bash
azd config set auth.useAzCliAuth true
azd provision --no-prompt
azd deploy --no-prompt
```
