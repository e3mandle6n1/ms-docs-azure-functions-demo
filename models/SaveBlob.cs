using System.Runtime.Serialization;

namespace My.Function.Models;

public record SaveBlobRequest(string Filename, string Content);

public record SaveBlobResponse(string Filename, string BlobUri, string ContentType, int SizeBytes);

public enum BlobContentType
{
    [EnumMember(Value = "text/plain")]
    TextPlain,

    [EnumMember(Value = "application/json")]
    ApplicationJson,

    [EnumMember(Value = "text/html")]
    TextHtml,

    [EnumMember(Value = "text/csv")]
    TextCsv
}
