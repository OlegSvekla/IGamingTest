using IGamingTest.Core.Mappers;

namespace IGamingTest.Core.Http.Out.Mappers;

public interface IHttpResponseMessageToBoolMapper
    : IMapper<HttpResponseMessage, bool>;

public class HttpResponseMessageToBoolMapper
    : IHttpResponseMessageToBoolMapper
{
    public bool Map(HttpResponseMessage input)
        => input.IsSuccessStatusCode;
}
