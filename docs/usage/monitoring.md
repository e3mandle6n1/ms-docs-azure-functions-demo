# Monitoring (KQL)

Telemetry flows through OpenTelemetry to Application Insights (ingestion lags ~2–5 minutes). Run these in **Application Insights → Logs** in the portal.

## Recent heartbeats

```kusto
traces
| where message startswith "Heartbeat at"
| project timestamp, message, severityLevel, operation_Name, cloud_RoleName
| order by timestamp desc
```

## All Heartbeat function logs

```kusto
traces
| where customDimensions.CategoryName == "My.Function.Heartbeat"
   or operation_Name == "Heartbeat"
| order by timestamp desc
```

## Heartbeat rate chart

```kusto
traces
| where message startswith "Heartbeat at"
| summarize executions = count() by bin(timestamp, 10m)
| render timechart
```

> Querying the Log Analytics workspace directly instead of the Application Insights resource? Use the `AppTraces` table with `TimeGenerated` / `Message` in place of `traces` / `timestamp` / `message`.
