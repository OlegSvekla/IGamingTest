using System.Text;
using Microsoft.AspNetCore.Http;

namespace IGamingTest.Core.Http.In.Helpers;

public static class HttpContextHelper
{
    public static async Task<string> ReadRequestBodyAsStringAsync(
        this HttpContext context,
        CancellationToken ct)
    {
        context.Request.EnableBuffering();
        var body = context.Request.Body;
        body.Position = 0;

        using var reader = new StreamReader(
            stream: body,
            encoding: Encoding.UTF8,
            leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync(ct);

        return requestBody;
    }
}
