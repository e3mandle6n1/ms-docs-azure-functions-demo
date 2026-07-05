using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;
using My.Function.Services;

namespace My.Function;

public class UploadAndProcess
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly IBlobUploadService _blobUploadService;
    private readonly ILogger<UploadAndProcess> _logger;

    public UploadAndProcess(IBlobUploadService blobUploadService, ILogger<UploadAndProcess> logger)
    {
        _blobUploadService = blobUploadService;
        _logger = logger;
    }

    [Function("UploadAndProcess")]
    [Consumes("multipart/form-data")]
    public async Task<UploadAndProcessOutput> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload")] HttpRequest req,
        IFormFile? file)
    {
        var (blob, error) = await _blobUploadService.SaveFileAsync(file, req.HttpContext.RequestAborted);
        if (error is not null)
        {
            return BadRequest(error);
        }

        var job = UploadJobPayload.FromBlob(blob!);

        _logger.LogInformation(
            "Upload job {JobId}: saved {BlobUri} and enqueueing to {QueueName}",
            job.Id,
            job.BlobUri,
            ProcessQueueMessage.QueueName);

        return new UploadAndProcessOutput
        {
            QueueMessage = JsonSerializer.Serialize(job, SerializerOptions),
            HttpResponse = new AcceptedResult(
                (string?)null,
                new UploadAndProcessResponse(job.Id, ProcessQueueMessage.QueueName, blob!))
        };
    }

    private static UploadAndProcessOutput BadRequest(string error) => new()
    {
        HttpResponse = new BadRequestObjectResult(new { error })
    };
}

public class UploadAndProcessOutput
{
    [QueueOutput(ProcessQueueMessage.QueueName)]
    public string? QueueMessage { get; set; }

    [HttpResult]
    public IActionResult HttpResponse { get; set; } = null!;
}
