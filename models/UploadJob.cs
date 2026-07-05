namespace My.Function.Models;

public record UploadJobPayload(
    Guid Id,
    string Filename,
    string BlobUri,
    string ContentType,
    int SizeBytes,
    DateTimeOffset UploadedAt)
{
    public static UploadJobPayload FromBlob(SaveBlobResponse blob) =>
        new(
            Guid.NewGuid(),
            blob.Filename,
            blob.BlobUri,
            blob.ContentType,
            blob.SizeBytes,
            DateTimeOffset.UtcNow);
}

public record UploadAndProcessResponse(
    Guid JobId,
    string Queue,
    SaveBlobResponse Blob);
