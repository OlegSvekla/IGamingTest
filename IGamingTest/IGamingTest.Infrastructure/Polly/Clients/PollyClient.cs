using IGamingTest.Core.Http.Out.Helpers;
using IGamingTest.Infrastructure.Polly.Clients.Loggers;
using IGamingTest.Infrastructure.Polly.Clients.Policies;
using Microsoft.Extensions.Logging;
using Polly;

namespace IGamingTest.Infrastructure.Polly.Clients;

public interface IPollyClient
{
    Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default);

    Task<HttpResponseMessage> SendFormAsync(
        Uri requestUri,
        FormUrlEncodedContent content,
        CancellationToken ct = default);

    Task<HttpResponseMessage> SendFormAsync(
        Uri requestUri,
        MultipartFormDataContent content,
        CancellationToken ct = default);
}

public class PollyClient(
    IEnumerable<IPolicySetup> policySetups,
    IHttpClientFactory clientFactory,
    ILogger<PollyClient> logger
    ) : IPollyClient
{
    public async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default)
    {
        var pb = new ResiliencePipelineBuilder<HttpResponseMessage>();
        var pipeline = policySetups
            .OrderBy(policySetup => policySetup.PipelineOrder)
            .Aggregate(pb, (builder, policySetup) => policySetup.SetPolicy(builder))
            .Build();

        var rs = await pipeline.ExecuteAsync(InnerSendAsync, rq, ct);
        return rs;
    }

    public async Task<HttpResponseMessage> SendFormAsync(
        Uri uri,
        FormUrlEncodedContent content,
        CancellationToken ct = default)
    {
        var pb = new ResiliencePipelineBuilder<HttpResponseMessage>();
        var pipeline = policySetups
            .OrderBy(policySetup => policySetup.PipelineOrder)
            .Aggregate(pb, (builder, policySetup) => policySetup.SetPolicy(builder))
            .Build();

        var rs = await pipeline.ExecuteAsync(InnerSendAsync, (uri, content), ct);
        return rs;
    }

    public async Task<HttpResponseMessage> SendFormAsync(
        Uri requestUri, 
        MultipartFormDataContent content,
        CancellationToken ct = default)
    {
        var pb = new ResiliencePipelineBuilder<HttpResponseMessage>();
        var pipeline = policySetups
            .OrderBy(policySetup => policySetup.PipelineOrder)
            .Aggregate(pb, (builder, policySetup) => policySetup.SetPolicy(builder))
        .Build();

        var rs = await pipeline.ExecuteAsync(InnerSendAsync, (requestUri, content), ct);
        return rs;
    }

    private async ValueTask<HttpResponseMessage> InnerSendAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default)
    {
        var rqDeepCopy = rq.DeepCopy();

        using var httpClient = clientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Host = rqDeepCopy.RequestUri?.Host;
        var rs = await httpClient.SendAsync(rqDeepCopy, ct);

        var rqLog = await HttpRequestMessageSerializeHelper.Serialize(rq);
        var rsLog = await HttpRequestMessageSerializeHelper.Serialize(rs);
        logger.LogInformation("Request: {rq},\nResponse: {rs}", rqLog, rsLog);

        return rs;
    }

    private async ValueTask<HttpResponseMessage> InnerSendAsync(
        (Uri Uri, FormUrlEncodedContent Content) rq,
        CancellationToken ct = default)
    {
        using var httpClient = clientFactory.CreateClient();
        var rs = await httpClient.PostAsync(rq.Uri, rq.Content, ct);

        var rqLog = await HttpRequestMessageSerializeHelper.Serialize(rq);
        var rsLog = await HttpRequestMessageSerializeHelper.Serialize(rs);
        logger.LogInformation("Request: {rq},\nResponse: {rs}", rqLog, rsLog);

        return rs;
    }

    private async ValueTask<HttpResponseMessage> InnerSendAsync(
        (Uri Uri, MultipartFormDataContent Content) rq,
        CancellationToken ct = default)
    {
        using var httpClient = clientFactory.CreateClient();
        var rs = await httpClient.PostAsync(rq.Uri, rq.Content, ct);

        var rqLog = await HttpRequestMessageSerializeHelper.Serialize(rq);
        var rsLog = await HttpRequestMessageSerializeHelper.Serialize(rs);
        logger.LogInformation("Request: {rq},\nResponse: {rs}", rqLog, rsLog);

        return rs;
    }
}
