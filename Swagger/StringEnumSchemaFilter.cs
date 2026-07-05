using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace My.Function.Swagger;

internal sealed class StringEnumSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum || schema is not OpenApiSchema mutableSchema)
        {
            return;
        }

        mutableSchema.Type = JsonSchemaType.String;
        mutableSchema.Format = null;
        mutableSchema.Enum = context.Type
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(field => JsonValue.Create(GetSerializedValue(field)))
            .Cast<JsonNode>()
            .ToList();
    }

    private static string GetSerializedValue(FieldInfo field)
    {
        var enumMember = field.GetCustomAttribute<EnumMemberAttribute>()?.Value;
        if (!string.IsNullOrWhiteSpace(enumMember))
        {
            return enumMember;
        }

        return JsonNamingPolicy.CamelCase.ConvertName(field.Name);
    }
}
