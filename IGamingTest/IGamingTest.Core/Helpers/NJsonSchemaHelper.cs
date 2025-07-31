using NJsonSchema;
using System.Reflection;
using IGamingTest.Core.Serializers;

namespace IGamingTest.Core.Helpers;

public static class NJsonSchemaHelper
{
    public static void HandleAttributes(this JsonSchema schema, Type type)
    {
        var recordAttributes = type.GetCustomAttributes().OfType<ISchemaClassAttribute>();
        foreach (var attribute in recordAttributes)
        {
            attribute.Apply(schema);
        }

        foreach (var prop in type.GetProperties())
        {
            if (schema.Properties.TryGetValue(prop.Name, out var schemaProperty))
            {
                var attributes = prop.GetCustomAttributes().OfType<ISchemaPropertyAttribute>();
                foreach (var attribute in attributes)
                {
                    attribute.Apply(schemaProperty);
                }
            }
        }
    }

    public static void HandleAttributes<T>(this JsonSchema schema)
    {
        var type = typeof(T);
        var recordAttributes = type.GetCustomAttributes().OfType<ISchemaClassAttribute>();
        foreach (var attribute in recordAttributes)
        {
            attribute.Apply(schema);
        }

        foreach (var prop in type.GetProperties())
        {
            if (schema.Properties.TryGetValue(prop.Name, out var schemaProperty))
            {
                var attributes = prop.GetCustomAttributes().OfType<ISchemaPropertyAttribute>();
                foreach (var attribute in attributes)
                {
                    attribute.Apply(schemaProperty);
                }
            }
        }
    }
}
