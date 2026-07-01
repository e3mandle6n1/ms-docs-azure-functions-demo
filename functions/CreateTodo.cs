using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;

namespace My.Function;

public class CreateTodo
{
    private const int MaxTitleLength = 200;
    private readonly ILogger<CreateTodo> _logger;

    public CreateTodo(ILogger<CreateTodo> logger)
    {
        _logger = logger;
    }

    [Function("CreateTodo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todos")] HttpRequest req)
    {
        CreateTodoRequest? request;
        try
        {
            request = await JsonSerializer.DeserializeAsync<CreateTodoRequest>(req.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException)
        {
            return new BadRequestObjectResult(new { error = "Request body must be valid JSON." });
        }

        if (request is null || string.IsNullOrWhiteSpace(request.Title))
        {
            return new BadRequestObjectResult(new { error = "Field 'title' is required." });
        }

        var title = request.Title.Trim();
        if (title.Length > MaxTitleLength)
        {
            return new BadRequestObjectResult(new { error = $"Field 'title' must be at most {MaxTitleLength} characters." });
        }

        var todo = new Todo(Guid.NewGuid(), title, DateTimeOffset.UtcNow);

        _logger.LogInformation("Created todo {TodoId}: {Title}", todo.Id, todo.Title);

        return new CreatedResult($"/api/todos/{todo.Id}", todo);
    }
}