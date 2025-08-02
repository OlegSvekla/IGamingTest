namespace IGamingTest.Core.Http.Out.Mappers;

public interface IHttpResponseMessageToStreamRsMapper
{
    ValueTask<(bool Delivered, Stream? Rs)> MapAsync(
       HttpResponseMessage input,
       CancellationToken ct);
}

public class HttpResponseMessageToStreamRsMapper : IHttpResponseMessageToStreamRsMapper
{
    public async ValueTask<(bool Delivered, Stream? Rs)> MapAsync(
        HttpResponseMessage input, 
        CancellationToken ct)
    {
        if (!input.IsSuccessStatusCode)
        {
            return (false, default);
        }

        var rsContent = await input.Content.ReadAsStreamAsync();

        return (true, rsContent);
    }
}
