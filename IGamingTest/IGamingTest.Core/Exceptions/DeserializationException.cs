using Newtonsoft.Json;

namespace IGamingTest.Core.Exceptions;

public class DeserializationException(
    object @object,
    Exception? innerEx = null
    ) : DomainException(
        message: $"Deserialization failed with object: {JsonConvert.SerializeObject(@object)}",
        innerEx: innerEx)
{
    public override int Code => (int)CoreExCodes.Deserialization;
}
