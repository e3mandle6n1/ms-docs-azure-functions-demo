using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace My.Function.Swagger;

/// <summary>
/// Keeps <c>GET /health</c> at the top of the Swagger UI operation list.
/// </summary>
internal sealed class HealthFirstDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc.Paths is null || swaggerDoc.Paths.Count == 0)
        {
            return;
        }

        var ordered = swaggerDoc.Paths
            .OrderBy(p => IsHealthPath(p.Key) ? 0 : 1)
            .ThenBy(p => p.Key, StringComparer.OrdinalIgnoreCase);

        var paths = new OpenApiPaths();
        foreach (var (key, value) in ordered)
        {
            paths.Add(key, value);
        }

        swaggerDoc.Paths = paths;
    }

    private static bool IsHealthPath(string path) =>
        path.Contains("health", StringComparison.OrdinalIgnoreCase);
}
