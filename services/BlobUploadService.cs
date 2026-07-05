using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function.Services;

/// <summary>
/// Validates multipart file uploads and persists them to the <c>uploads</c> blob container.
/// </summary>
public class BlobUploadService : IBlobUploadService
{
    public const string ContainerName = "uploads";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<BlobUploadService> _logger;

    public BlobUploadService(BlobServiceClient blobServiceClient, ILogger<BlobUploadService> logger)
    {
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    public async Task<(SaveBlobResponse? Blob, string? Error)> SaveFileAsync(
        IFormFile? file,
        CancellationToken cancellationToken = default)
    {
        if (!BlobUploadValidation.TryValidate(file, out var filename, out var error))
        {
            return (null, error);
        }

        var content = await BlobUploadValidation.ReadBytesAsync(file!, cancellationToken);
        var mimeType = BlobUploadValidation.ResolveContentType(file!);
        var blobUri = $"{ContainerName}/{filename}";

        await BlobUploadStorage.UploadAsync(
            _blobServiceClient,
            ContainerName,
            filename,
            content,
            mimeType,
            cancellationToken);

        _logger.LogInformation(
            "Saved blob {BlobUri} ({ContentType}, {SizeBytes} bytes)",
            blobUri,
            mimeType,
            content.Length);

        return (new SaveBlobResponse(filename, blobUri, mimeType, content.Length), null);
    }
}
