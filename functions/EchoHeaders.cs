using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My.Function;

public class EchoHeaders
{
    private static readonly string[] InterestingHeaders =
    [
        "User-Agent",
        "Host",
        "Accept",
        "X-Forwarded-For",
        "X-Forwarded-Proto",
        "X-Forwarded-Host",
        "X-Original-URL",
        "X-ARR-LOG-ID",
        "CLIENT-IP",
        "DISGUISED-HOST"
    ];

    private readonly ILogger<EchoHeaders> _logger;

    public EchoHeaders(ILogger<EchoHeaders> logger)
    {
        _logger = logger;
    }

    [Function("EchoHeaders")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "echo-headers")] HttpRequest req)
    {
        var interestingOnly = req.Query["filter"].ToString()
            .Equals("interesting", StringComparison.OrdinalIgnoreCase);

        var headers = req.Headers
            .Where(h => !interestingOnly || InterestingHeaders.Contains(h.Key, StringComparer.OrdinalIgnoreCase))
            .OrderBy(h => h.Key, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(h => h.Key, h => h.Value.ToString());

        _logger.LogInformation("Echoed {Count} headers (interestingOnly: {InterestingOnly})", headers.Count, interestingOnly);

        return new OkObjectResult(new { count = headers.Count, headers });
    }
}
