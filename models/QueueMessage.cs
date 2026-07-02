namespace My.Function.Models;

public record QueueMessagePayload(Guid Id, string Message, DateTimeOffset EnqueuedAt);

public record EnqueueMessageRequest(string Message);
