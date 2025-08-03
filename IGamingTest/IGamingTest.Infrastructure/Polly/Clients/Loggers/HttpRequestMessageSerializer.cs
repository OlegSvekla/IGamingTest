using System.Globalization;
using System.Text;

namespace IGamingTest.Infrastructure.Polly.Clients.Loggers;

public class HttpRequestMessageSerializeHelper
{
    public static async Task<string> Serialize(
        HttpRequestMessage rq)
    {
        var requestLog = new StringBuilder();

        requestLog.AppendLine(CultureInfo.InvariantCulture, $"{rq.Method} {rq.RequestUri}");

        foreach (var header in rq.Headers)
        {
            requestLog.AppendLine(CultureInfo.InvariantCulture, $"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (rq.Content != null)
        {
            var content = await rq.Content.ReadAsStringAsync();
            requestLog.AppendLine(CultureInfo.InvariantCulture, $"Content: {content}");
        }

        return requestLog.ToString();
    }

    public static async Task<string> Serialize(
        (Uri Uri, FormUrlEncodedContent Content) rq)
    {
        var content = await rq.Content.ReadAsStringAsync();

        var requestLog = new StringBuilder();
        requestLog.AppendLine(CultureInfo.InvariantCulture, $"POST {rq.Uri}");
        requestLog.AppendLine(CultureInfo.InvariantCulture, $"Content: {content}");
        return requestLog.ToString();
    }

    public static async Task<string> Serialize(
    (Uri Uri, MultipartFormDataContent Content) rq)
    {
        var content = await rq.Content.ReadAsStringAsync();

        var requestLog = new StringBuilder();
        requestLog.AppendLine(CultureInfo.InvariantCulture, $"POST {rq.Uri}");
        requestLog.AppendLine(CultureInfo.InvariantCulture, $"Content: {content}");
        return requestLog.ToString();
    }

    public static async Task<string> Serialize(
        HttpResponseMessage rs)
    {
        var responseLog = new StringBuilder();

        responseLog.AppendLine(CultureInfo.InvariantCulture, $"Status Code: {rs.StatusCode}");

        foreach (var header in rs.Headers)
        {
            responseLog.AppendLine(CultureInfo.InvariantCulture, $"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (rs.Content != null)
        {
            var content = await rs.Content.ReadAsStringAsync();
            responseLog.AppendLine(CultureInfo.InvariantCulture, $"Content: {content}");
        }

        return responseLog.ToString();
    }
}
