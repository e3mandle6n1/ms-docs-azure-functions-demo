namespace My.Function.Models;

public record SaveBlobResponse(string Filename, string BlobUri, string ContentType, int SizeBytes);
