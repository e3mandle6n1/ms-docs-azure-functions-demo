using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace My.Function.ModelBinding;

internal sealed class EnumMemberModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var modelType = Nullable.GetUnderlyingType(context.Metadata.ModelType) ?? context.Metadata.ModelType;
        return modelType.IsEnum ? new EnumMemberModelBinder(modelType) : null;
    }
}

internal sealed class EnumMemberModelBinder(Type enumType) : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;
        if (string.IsNullOrWhiteSpace(value))
        {
            return Task.CompletedTask;
        }

        if (TryParse(value, out var result))
        {
            bindingContext.Result = ModelBindingResult.Success(result);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(
                bindingContext.ModelName,
                $"'{value}' is not a valid {enumType.Name} value.");
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }

    private bool TryParse(string value, out object? result)
    {
        result = null;
        if (Enum.TryParse(enumType, value, ignoreCase: true, out var byName))
        {
            result = byName;
            return true;
        }

        foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var memberValue = field.GetCustomAttribute<EnumMemberAttribute>()?.Value;
            if (memberValue is not null &&
                memberValue.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = field.GetValue(null);
                return true;
            }
        }

        return false;
    }
}
