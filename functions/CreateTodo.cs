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
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todos")] HttpRequest req,
        [FromBody] IReadOnlyList<CreateTodoRequest>? todos)
    {
        if (todos is null || todos.Count == 0)
        {
            return new BadRequestObjectResult(new { error = "Request body must be a non-empty JSON array of todos." });
        }

        var created = new List<Todo>(todos.Count);

        for (var i = 0; i < todos.Count; i++)
        {
            var item = todos[i];
            if (string.IsNullOrWhiteSpace(item?.Title))
            {
                return new BadRequestObjectResult(new { error = $"Item at index {i} is missing required field 'title'." });
            }

            var title = item.Title.Trim();
            if (title.Length > MaxTitleLength)
            {
                return new BadRequestObjectResult(new
                {
                    error = $"Item at index {i}: field 'title' must be at most {MaxTitleLength} characters."
                });
            }

            created.Add(new Todo(Guid.NewGuid(), title, DateTimeOffset.UtcNow));
        }

        foreach (var todo in created)
        {
            _logger.LogInformation("Created todo {TodoId}: {Title}", todo.Id, todo.Title);
        }

        return new CreatedResult("/api/todos", new CreateTodosResponse(created));
    }
}
