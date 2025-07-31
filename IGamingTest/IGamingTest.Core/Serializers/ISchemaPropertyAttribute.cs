using NJsonSchema;

namespace IGamingTest.Core.Serializers;

public interface ISchemaPropertyAttribute
{
    void Apply(JsonSchema schema);
}
