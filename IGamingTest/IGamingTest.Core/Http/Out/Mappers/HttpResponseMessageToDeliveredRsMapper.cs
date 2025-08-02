using IGamingTest.Core.Serializers;
using System.Globalization;

namespace IGamingTest.Core.Http.Out.Mappers;

public interface IHttpResponseMessageToDeliveredRsMapper
{
    ValueTask<(bool Delivered, TRs? Rs)> MapAsync<TRs>(
        HttpResponseMessage input,
        CancellationToken ct);
}

public class HttpResponseMessageToDeliveredRsMapper(
    ISerializer<string> serializer
    ) : IHttpResponseMessageToDeliveredRsMapper
{
    public async ValueTask<(bool Delivered, TRs? Rs)> MapAsync<TRs>(HttpResponseMessage input, CancellationToken ct)
    {
        if (!input.IsSuccessStatusCode)
        {
            return (false, default);
        }

        var rsContent = await input.Content.ReadAsStringAsync(ct);

        if (typeof(TRs) == typeof(string))
        {
            var rsStr = (TRs)Convert.ChangeType(rsContent, typeof(TRs), CultureInfo.InvariantCulture)!;
            return (true, rsStr);
        }

        var rs = serializer.ForceDeserialize<TRs>(rsContent);
        return (true, rs);
    }
}
