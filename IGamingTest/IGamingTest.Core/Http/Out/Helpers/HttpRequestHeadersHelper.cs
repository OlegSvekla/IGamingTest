using IGamingTest.Core.Helpers;
using System.Net;
using System.Net.Http.Headers;

namespace IGamingTest.Core.Http.Out.Helpers;

public static class HttpRequestHeadersHelper
{
    public static void AddRange(
        this HttpRequestHeaders rqHeaders,
        IDictionary<string, string>? headers)
    {
        if (headers == null)
        {
            return;
        }

        headers.ForEach(header => rqHeaders.Add(header.Key, header.Value));
    }

    public static void AddRange(
        this HttpRequestHeaders rqHeaders,
        HttpRequestHeaders headers)
    {
        if (headers == null)
        {
            return;
        }

        headers.ForEach(header => rqHeaders.Add(header.Key, header.Value));
    }

    public static void AddKeyValuePair(
        this IDictionary<string, string> headers,
        KeyValuePair<string, string> keyValuePair)
        => headers.Add(keyValuePair.Key, keyValuePair.Value);

    public static KeyValuePair<string, string> AddAuthBearerToken(
        string authToken)
        => new KeyValuePair<string, string>(
            key: nameof(HttpRequestHeader.Authorization),
            value: authToken);

    public static IDictionary<string, string> AddAuthBearerToken(
        this IDictionary<string, string> headers,
        string authToken)
    {
        var keyValuePair = AddAuthBearerToken(authToken);
        headers.AddKeyValuePair(keyValuePair);
        return headers;
    }

    public static IDictionary<string, string> TryAddAuthBearerToken(
        this IDictionary<string, string> headers,
        string? authToken)
    {
        if (!string.IsNullOrWhiteSpace(authToken))
        {
            headers.AddAuthBearerToken(authToken);
        }

        return headers;
    }
}
