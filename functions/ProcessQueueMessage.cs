using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace My.Function;

public class ProcessQueueMessage
{
    private readonly ILogger<ProcessQueueMessage> _logger;

    public ProcessQueueMessage(ILogger<ProcessQueueMessage> logger)
    {
        _logger = logger;
    }

    [Function("ProcessQueueMessage")]
    public void Run([QueueTrigger("demo-queue")] QueueMessage message)
    {
        _logger.LogInformation(
            "Processing queue message {MessageId} (dequeue count: {DequeueCount}, inserted: {InsertedOn:O}): {Body}",
            message.MessageId,
            message.DequeueCount,
            message.InsertedOn,
            message.Body.ToString());
    }
}
