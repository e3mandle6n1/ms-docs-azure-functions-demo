using My.Function.Models;

namespace My.Function.Repositories;

public interface ITodoRepository
{
    Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddRangeAsync(IReadOnlyList<Todo> todos, CancellationToken cancellationToken = default);
}
