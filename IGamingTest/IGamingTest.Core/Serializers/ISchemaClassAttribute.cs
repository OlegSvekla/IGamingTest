using NJsonSchema;

namespace IGamingTest.Core.Serializers;

public interface ISchemaClassAttribute
{
    void Apply(JsonSchema schema);
}
