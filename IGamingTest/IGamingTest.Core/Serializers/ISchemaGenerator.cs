using NJsonSchema;

namespace IGamingTest.Core.Serializers;

public interface ISchemaGenerator
{
    JsonSchema GenerateSchema<T>();

    JsonSchema GenerateSchema(Type type);
}
