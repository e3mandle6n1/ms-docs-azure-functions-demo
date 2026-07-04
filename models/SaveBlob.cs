namespace My.Function.Models;

public record SaveBlobRequest(string Filename, string Content, string? ContentType);

public record SaveBlobResponse(string Filename, string BlobUri, string ContentType, int SizeBytes);
