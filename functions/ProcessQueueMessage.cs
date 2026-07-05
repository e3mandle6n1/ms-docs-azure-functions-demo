using System.Text.Json;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function;

public class ProcessQueueMessage
{
    public const string QueueName = "demo-queue";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ILogger<ProcessQueueMessage> _logger;

    public ProcessQueueMessage(ILogger<ProcessQueueMessage> logger)
    {
        _logger = logger;
    }

    [Function("ProcessQueueMessage")]
    public void Run([QueueTrigger(QueueName)] QueueMessage message)
    {
        var body = message.Body.ToString();

        if (TryParseUploadJob(body, out var job))
        {
            _logger.LogInformation(
                "Processing upload job {JobId} for {Filename} at {BlobUri} ({ContentType}, {SizeBytes} bytes; dequeue count: {DequeueCount}, uploaded: {UploadedAt:O})",
                job.Id,
                job.Filename,
                job.BlobUri,
                job.ContentType,
                job.SizeBytes,
                message.DequeueCount,
                job.UploadedAt);
            return;
        }

        _logger.LogInformation(
            "Processing queue message {MessageId} (dequeue count: {DequeueCount}, inserted: {InsertedOn:O}): {Body}",
            message.MessageId,
            message.DequeueCount,
            message.InsertedOn,
            body);
    }

    private static bool TryParseUploadJob(string? body, out UploadJobPayload job)
    {
        job = null!;

        if (string.IsNullOrWhiteSpace(body))
        {
            return false;
        }

        try
        {
            var parsed = JsonSerializer.Deserialize<UploadJobPayload>(body, SerializerOptions);
            if (parsed is null || parsed.Id == Guid.Empty || string.IsNullOrWhiteSpace(parsed.Filename))
            {
                return false;
            }

            job = parsed;
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
