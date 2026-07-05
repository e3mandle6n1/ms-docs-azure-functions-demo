using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function;

public class HealthCheck
{
    public const string AppVersion = "1.0.0";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<HealthCheck> _logger;

    public HealthCheck(BlobServiceClient blobServiceClient, ILogger<HealthCheck> logger)
    {
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    [Function("HealthCheck")]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HealthCheckResponse), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        var storage = await CheckStorageAsync(req.HttpContext.RequestAborted);
        var healthy = storage == "ok";

        var response = new HealthCheckResponse(
            healthy ? "healthy" : "unhealthy",
            storage,
            AppVersion);

        _logger.LogInformation("Health check: status={Status}, storage={Storage}", response.Status, response.Storage);

        return healthy
            ? new OkObjectResult(response)
            : new ObjectResult(response) { StatusCode = StatusCodes.Status503ServiceUnavailable };
    }

    private async Task<string> CheckStorageAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _blobServiceClient.GetPropertiesAsync(cancellationToken);
            return "ok";
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Storage health check failed");
            return "error";
        }
    }
}
