using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using My.Function.Models;
using My.Function.Repositories;

namespace My.Function;

public class GetTodos
{
    private readonly ITodoRepository _todoRepository;
    private readonly ILogger<GetTodos> _logger;

    public GetTodos(ITodoRepository todoRepository, ILogger<GetTodos> logger)
    {
        _todoRepository = todoRepository;
        _logger = logger;
    }

    [Function("GetTodos")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todos")] HttpRequest req)
    {
        var todos = await _todoRepository.GetAllAsync(req.HttpContext.RequestAborted);

        _logger.LogInformation("Returning {Count} todos", todos.Count);

        return new OkObjectResult(new CreateTodosResponse(todos));
    }
}
