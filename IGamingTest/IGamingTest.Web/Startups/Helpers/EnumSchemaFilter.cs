using IGamingTest.Core.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IGamingTest.Web.Startups.Helpers;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        foreach (var property in context.Type.GetProperties())
        {
            if (property.GetCustomAttributes(typeof(FromEnumAttribute), false)
                    .FirstOrDefault() is not FromEnumAttribute thisEnumAttribute)
            {
                continue;
            }

            var enumType = thisEnumAttribute.EnumType;
            if (enumType.IsEnum)
            {
                var names = enumType.GetEnumNames();
                var openApiList = names
                    .Select(name => new OpenApiString(name))
                    .ToList<IOpenApiAny>();

                var propertyName = char.ToLowerInvariant(property.Name[0]) + property.Name[1..];
                schema.Properties[propertyName].Enum = openApiList;
            }
        }
    }
}
