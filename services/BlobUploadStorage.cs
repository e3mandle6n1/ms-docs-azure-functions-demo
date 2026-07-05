using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace My.Function.Services;

public static class BlobUploadStorage
{
    public static async Task UploadAsync(
        BlobServiceClient blobServiceClient,
        string containerName,
        string filename,
        byte[] content,
        string contentType,
        CancellationToken cancellationToken)
    {
        var container = blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var blob = container.GetBlobClient(filename);
        await blob.UploadAsync(
            BinaryData.FromBytes(content),
            new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
            },
            cancellationToken);
    }
}
