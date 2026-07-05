using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using My.Function.Services;

namespace My.Function;

public class SaveToBlob
{
    private readonly IBlobUploadService _blobUploadService;

    public SaveToBlob(IBlobUploadService blobUploadService)
    {
        _blobUploadService = blobUploadService;
    }

    [Function("SaveToBlob")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "save")] HttpRequest req,
        IFormFile? file)
    {
        var (blob, error) = await _blobUploadService.SaveFileAsync(file, req.HttpContext.RequestAborted);
        if (error is not null)
        {
            return new BadRequestObjectResult(new { error });
        }

        return new CreatedResult($"/api/save/{blob!.Filename}", blob);
    }
}
