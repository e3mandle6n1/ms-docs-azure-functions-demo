using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function;

public class EnqueueMessage
{
    public const string QueueName = "demo-messages";
    private const int MaxMessageLength = 500;

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ILogger<EnqueueMessage> _logger;

    public EnqueueMessage(ILogger<EnqueueMessage> logger)
    {
        _logger = logger;
    }

    [Function("EnqueueMessage")]
    public Task<EnqueueMessageOutput> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "messages")] HttpRequest req,
        [FromBody] EnqueueMessageRequest? request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Field 'message' is required.");
        }

        var message = request.Message.Trim();
        if (message.Length > MaxMessageLength)
        {
            return BadRequest($"Field 'message' must be at most {MaxMessageLength} characters.");
        }

        var payload = new QueueMessagePayload(Guid.NewGuid(), message, DateTimeOffset.UtcNow);

        _logger.LogInformation("Enqueued message {MessageId} to queue {QueueName}", payload.Id, QueueName);

        return Task.FromResult(new EnqueueMessageOutput
        {
            QueueMessage = JsonSerializer.Serialize(payload, SerializerOptions),
            HttpResponse = new AcceptedResult((string?)null, new { queue = QueueName, payload })
        });
    }

    private static Task<EnqueueMessageOutput> BadRequest(string error) => Task.FromResult(new EnqueueMessageOutput
    {
        HttpResponse = new BadRequestObjectResult(new { error })
    });
}

public class EnqueueMessageOutput
{
    [QueueOutput(EnqueueMessage.QueueName)]
    public string? QueueMessage { get; set; }

    [HttpResult]
    public IActionResult HttpResponse { get; set; } = null!;
}
