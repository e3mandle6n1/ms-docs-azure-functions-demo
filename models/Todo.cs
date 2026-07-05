namespace My.Function.Models;

public record Todo(Guid Id, string Title, DateTimeOffset CreatedAt);

public record CreateTodoRequest(string Title);

public record CreateTodosResponse(IReadOnlyList<Todo> Todos);