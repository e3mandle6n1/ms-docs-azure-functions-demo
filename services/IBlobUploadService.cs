using Microsoft.AspNetCore.Http;
using My.Function.Models;

namespace My.Function.Services;

public interface IBlobUploadService
{
    Task<(SaveBlobResponse? Blob, string? Error)> SaveFileAsync(
        IFormFile? file,
        CancellationToken cancellationToken = default);
}
