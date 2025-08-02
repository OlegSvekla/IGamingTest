using IGamingTest.Core.Http.Out.Models;
using IGamingTest.Core.Serializers;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace IGamingTest.Core.Http.Out.Mappers;

public interface IHttpWithBodyRqToMessageMapper
{
    HttpRequestMessage Map<TBody>(HttpWithBodyRq<TBody> input);
}

public class HttpWithBodyRqToMessageMapper(
    IHttpRqToMessageMapper httpRqToMessageMapper,
    ISerializer<string> serializer
    ) : IHttpWithBodyRqToMessageMapper
{
    public HttpRequestMessage Map<TBody>(HttpWithBodyRq<TBody> input)
    {
        var content = SerializeBody(serializer, input);

        var rq = httpRqToMessageMapper.Map(input);
        rq.Content = content;

        return rq;
    }

    private static StringContent SerializeBody<TBody>(
        ISerializer<string> serializer,
        HttpWithBodyRq<TBody> input)
    {
        var bodyStr = serializer.Serialize(input.Body!);
        var mediaType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
        var content = new StringContent(bodyStr, mediaType);
        content.Headers.ContentLength = Encoding.UTF8.GetByteCount(bodyStr);
        return content;
    }
}
