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
    public Task<SaveToBlobOutput> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "save")] HttpRequest req,
        [FromBody] SaveBlobRequest? request,
        [FromQuery] BlobContentType contentType = BlobContentType.TextPlain)
    {
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

        var mimeType = ToMimeType(contentType);

        var blobUri = $"{ContainerName}/{filename}";

        _logger.LogInformation("Saving blob {BlobUri} ({ContentType}, {SizeBytes} bytes)", blobUri, mimeType, request.Content.Length);

        return Task.FromResult(new SaveToBlobOutput
        {
            Filename = filename,
            BlobContent = request.Content,
            HttpResponse = new CreatedResult(
                $"/api/save/{filename}",
                new SaveBlobResponse(filename, blobUri, mimeType, request.Content.Length))
        });
    }

    private static string ToMimeType(BlobContentType contentType) => contentType switch
    {
        BlobContentType.ApplicationJson => "application/json",
        BlobContentType.TextHtml => "text/html",
        BlobContentType.TextCsv => "text/csv",
        _ => "text/plain"
    };

    private static bool IsSafeFilename(string filename) =>
        !filename.Contains('/') &&
        !filename.Contains('\\') &&
        !filename.Contains("..", StringComparison.Ordinal);

    private static Task<SaveToBlobOutput> BadRequest(string error) => Task.FromResult(new SaveToBlobOutput
    {
        HttpResponse = new BadRequestObjectResult(new { error })
    });
}

public class SaveToBlobOutput
{
    public string? Filename { get; set; }

    [BlobOutput(SaveToBlob.BlobPathPattern)]
    public string? BlobContent { get; set; }

    [HttpResult]
    public IActionResult HttpResponse { get; set; } = null!;
}
