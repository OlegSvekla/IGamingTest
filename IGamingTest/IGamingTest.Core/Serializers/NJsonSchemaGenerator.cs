using IGamingTest.Core.Helpers;
using NJsonSchema;

namespace IGamingTest.Core.Serializers;

public class NJsonSchemaGenerator
    : ISchemaGenerator

{
    public JsonSchema GenerateSchema<T>()
    {
        var schema = JsonSchema.FromType<T>();

        schema.HandleAttributes<T>();
        return schema;
    }

    public JsonSchema GenerateSchema(Type type)
    {
        var schema = JsonSchema.FromType(type);

        schema.HandleAttributes(type);
        return schema;
    }
}
