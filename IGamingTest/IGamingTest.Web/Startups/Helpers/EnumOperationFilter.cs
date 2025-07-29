using IGamingTest.Core.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IGamingTest.Web.Startups.Helpers;

public class EnumOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Process path parameters annotated with FromEnumAttribute
        AddEnumDescriptionsToPathParameters(operation, context);

        // Process body parameters annotated with FromEnumAttribute
        AddEnumDescriptionsToBodyParameters(operation, context);
    }

    private static void AddEnumDescriptionsToPathParameters(
        OpenApiOperation operation,
        OperationFilterContext context)
    {
        foreach (var parameter in operation.Parameters)
        {
            if (context.MethodInfo
                .GetParameters()
                .FirstOrDefault(p => p.Name == parameter.Name)?
                .GetCustomAttributes(typeof(FromEnumAttribute), false)
                .FirstOrDefault() is not FromEnumAttribute fromEnumAttribute)
            {
                continue;
            }

            // Extract enum values and build the description
            var enumType = fromEnumAttribute.EnumType;
            var enumValues = enumType.GetEnumNames();
            var enumDescription = $"{parameter.Name} is enum value: [ {string.Join(", ", enumValues)} ]<br/>";

            // Add description and examples to the parameter
            parameter.Description = AppendToDescription(parameter.Description, enumDescription);
            parameter.Examples = enumValues.ToDictionary(
                val => val,
                val => new OpenApiExample { Value = new OpenApiString(val) });
        }
    }

    private static void AddEnumDescriptionsToBodyParameters(
        OpenApiOperation operation,
        OperationFilterContext context)
    {
        foreach (var bodyParameter in context.ApiDescription.ParameterDescriptions)
        {
            if (bodyParameter.Type is null)
            {
                continue;
            }

            foreach (var property in bodyParameter.Type.GetProperties())
            {
                if (property
                    .GetCustomAttributes(typeof(FromEnumAttribute), false)
                    .FirstOrDefault() is not FromEnumAttribute fromEnumAttribute)
                {
                    continue;
                }

                // Extract enum values and build the description
                var enumType = fromEnumAttribute.EnumType;
                var enumValues = enumType.GetEnumNames();
                var enumDescription = $"{property.Name} is enum value: [ {string.Join(", ", enumValues)} ]<br/>";

                // Add description to the operation
                operation.Description = AppendToDescription(operation.Description, enumDescription);
            }
        }
    }

    private static string AppendToDescription(string existingDescription, string additionalDescription)
        => string.IsNullOrWhiteSpace(existingDescription)
            ? additionalDescription
            : $"{existingDescription} {additionalDescription}".Trim();
}
