using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function;

public class SaveToBlob
{
    public const string ContainerName = "uploads";
    public const string BlobPathPattern = "uploads/{Filename}";

    private const int MaxFilenameLength = 255;
    private const int MaxContentLength = 1024 * 1024;

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ILogger<SaveToBlob> _logger;

    public SaveToBlob(ILogger<SaveToBlob> logger)
    {
        _logger = logger;
    }

    //POST:{host}:{port}/api/save -> SaveToBlobOutput
    [Function("SaveToBlob")]
    public async Task<SaveToBlobOutput> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "save")] HttpRequest req)
    {
        SaveBlobRequest? request;
        try
        {
            request = await JsonSerializer.DeserializeAsync<SaveBlobRequest>(req.Body, SerializerOptions);
        }
        catch (JsonException)
        {
            return BadRequest("Request body must be valid JSON.");
        }

        if (request is null || string.IsNullOrWhiteSpace(request.Filename))
        {
            return BadRequest("Field 'filename' is required.");
        }

        var filename = request.Filename.Trim();
        if (filename.Length > MaxFilenameLength)
        {
            return BadRequest($"Field 'filename' must be at most {MaxFilenameLength} characters.");
        }

        if (!IsSafeFilename(filename))
        {
            return BadRequest("Field 'filename' must not contain path separators or parent-directory segments.");
        }

        if (request.Content is null)
        {
            return BadRequest("Field 'content' is required.");
        }

        if (request.Content.Length > MaxContentLength)
        {
            return BadRequest($"Field 'content' must be at most {MaxContentLength} characters.");
        }

        var contentType = string.IsNullOrWhiteSpace(request.ContentType)
            ? "text/plain"
            : request.ContentType.Trim();

        var blobUri = $"{ContainerName}/{filename}";

        _logger.LogInformation("Saving blob {BlobUri} ({ContentType}, {SizeBytes} bytes)", blobUri, contentType, request.Content.Length);

        return new SaveToBlobOutput
        {
            Filename = filename,
            BlobContent = request.Content,
            HttpResponse = new CreatedResult(
                $"/api/save/{filename}",
                new SaveBlobResponse(filename, blobUri, contentType, request.Content.Length))
        };
    }

    private static bool IsSafeFilename(string filename) =>
        !filename.Contains('/') &&
        !filename.Contains('\\') &&
        !filename.Contains("..", StringComparison.Ordinal);

    private static SaveToBlobOutput BadRequest(string error) => new()
    {
        HttpResponse = new BadRequestObjectResult(new { error })
    };
}

public class SaveToBlobOutput
{
    public string? Filename { get; set; }

    [BlobOutput(SaveToBlob.BlobPathPattern)]
    public string? BlobContent { get; set; }

    [HttpResult]
    public IActionResult HttpResponse { get; set; } = null!;
}
