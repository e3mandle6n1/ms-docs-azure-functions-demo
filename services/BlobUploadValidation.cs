using Microsoft.AspNetCore.Http;

namespace My.Function.Services;

public static class BlobUploadValidation
{
    public const int MaxFilenameLength = 255;
    public const int MaxFileSizeBytes = 8 * 1024 * 1024;

    public static bool TryValidate(IFormFile? file, out string filename, out string error)
    {
        filename = string.Empty;
        error = string.Empty;

        if (file is null)
        {
            error = "Field 'file' is required.";
            return false;
        }

        if (file.Length == 0)
        {
            error = "Field 'file' must not be empty.";
            return false;
        }

        if (file.Length > MaxFileSizeBytes)
        {
            error = $"File must be at most {MaxFileSizeBytes} bytes.";
            return false;
        }

        filename = Path.GetFileName(file.FileName.Trim());
        if (string.IsNullOrWhiteSpace(filename))
        {
            error = "Uploaded file must include a filename.";
            return false;
        }

        if (filename.Length > MaxFilenameLength)
        {
            error = $"Filename must be at most {MaxFilenameLength} characters.";
            return false;
        }

        if (!IsSafeFilename(filename))
        {
            error = "Filename must not contain path separators or parent-directory segments.";
            return false;
        }

        return true;
    }

    public static async Task<byte[]> ReadBytesAsync(IFormFile file, CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();
        using var memory = new MemoryStream();
        await stream.CopyToAsync(memory, cancellationToken);
        return memory.ToArray();
    }

    public static string ResolveContentType(IFormFile file) =>
        string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType;

    private static bool IsSafeFilename(string filename) =>
        !filename.Contains('/') &&
        !filename.Contains('\\') &&
        !filename.Contains("..", StringComparison.Ordinal);
}
