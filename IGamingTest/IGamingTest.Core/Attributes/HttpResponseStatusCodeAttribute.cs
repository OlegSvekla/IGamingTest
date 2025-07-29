namespace IGamingTest.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class HttpResponseStatusCodeAttribute(
    int statusCode
    ) : Attribute
{
    public int StatusCode { get; } = statusCode;
}
