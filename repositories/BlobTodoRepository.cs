using System.Text.Json;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function.Repositories;

/// <summary>
/// Persists todos as a JSON array in the <c>uploads</c> blob container.
/// Uses optimistic concurrency (ETags) so concurrent writes retry instead of overwriting.
/// </summary>
public class BlobTodoRepository : ITodoRepository
{
    public const string ContainerName = "uploads";
    public const string BlobName = "todos.json";

    private const int MaxWriteAttempts = 5;

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly BlobContainerClient _container;
    private readonly ILogger<BlobTodoRepository> _logger;

    public BlobTodoRepository(BlobServiceClient blobServiceClient, ILogger<BlobTodoRepository> logger)
    {
        _container = blobServiceClient.GetBlobContainerClient(ContainerName);
        _logger = logger;
    }

    public async Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var blob = _container.GetBlobClient(BlobName);

        if (!await blob.ExistsAsync(cancellationToken))
        {
            return [];
        }

        var response = await blob.DownloadContentAsync(cancellationToken);
        var todos = JsonSerializer.Deserialize<List<Todo>>(response.Value.Content, SerializerOptions);

        return todos ?? [];
    }

    public async Task AddRangeAsync(IReadOnlyList<Todo> todos, CancellationToken cancellationToken = default)
    {
        await _container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var blob = _container.GetBlobClient(BlobName);

        for (var attempt = 1; attempt <= MaxWriteAttempts; attempt++)
        {
            var merged = new List<Todo>();
            BlobRequestConditions? conditions = null;

            if (await blob.ExistsAsync(cancellationToken))
            {
                var download = await blob.DownloadAsync(cancellationToken);
                conditions = new BlobRequestConditions { IfMatch = download.Value.Details.ETag };

                await using var stream = download.Value.Content;
                var current = await JsonSerializer.DeserializeAsync<List<Todo>>(stream, SerializerOptions, cancellationToken);
                if (current is not null)
                {
                    merged.AddRange(current);
                }
            }
            else
            {
                conditions = new BlobRequestConditions { IfNoneMatch = ETag.All };
            }

            merged.AddRange(todos);

            try
            {
                await blob.UploadAsync(
                    BinaryData.FromBytes(JsonSerializer.SerializeToUtf8Bytes(merged, SerializerOptions)),
                    new BlobUploadOptions
                    {
                        HttpHeaders = new BlobHttpHeaders { ContentType = "application/json" },
                        Conditions = conditions
                    },
                    cancellationToken);

                _logger.LogInformation(
                    "Appended {AddedCount} todo(s) to blob {Container}/{BlobName} ({TotalCount} total)",
                    todos.Count,
                    ContainerName,
                    BlobName,
                    merged.Count);

                return;
            }
            catch (RequestFailedException ex) when (ex.Status is 409 or 412)
            {
                _logger.LogWarning(
                    ex,
                    "Concurrent write to {Container}/{BlobName}; retrying ({Attempt}/{MaxAttempts})",
                    ContainerName,
                    BlobName,
                    attempt,
                    MaxWriteAttempts);
            }
        }

        throw new InvalidOperationException(
            $"Failed to append todos to {ContainerName}/{BlobName} after {MaxWriteAttempts} attempts.");
    }
}
