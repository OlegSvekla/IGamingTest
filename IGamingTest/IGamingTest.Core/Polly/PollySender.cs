using IGamingTest.Core.Http.Out;
using IGamingTest.Core.Http.Out.Mappers;
using IGamingTest.Core.Http.Out.Models;
using IGamingTest.Core.Polly.Clients;
using IGamingTest.Core.Polly.Exceptions;

namespace IGamingTest.Core.Http.Polly;

public class PollySender(
    IPollyClient pollyClient,
    IHttpResponseMessageToBoolMapper httpResponseMessageToBoolMapper,
    IHttpResponseMessageToDeliveredRsMapper httpResponseMessageToDeliveredRsMapper,
    IHttpRqToMessageMapper httpRqToMessageMapper,
    IHttpWithBodyRqToMessageMapper httpWithBodyRqToMessageMapper,
    IHttpResponseMessageToStreamRsMapper httpResponseMessageToStreamRsMapper
) : IHttpSender
{
    public async Task ForceSendAsync(
        HttpRq rq,
        CancellationToken ct = default)
    {
        var delivered = await SendAsync(rq, ct);
        HttpRqNotDeliveriedException.ThrowWhenNotDileveredSuccessfully(delivered, rq.Uri, rq.Method);
    }

    public async Task ForceSendAsync<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default)
    {
        var delivered = await SendAsync(rq, ct);
        HttpRqNotDeliveriedException.ThrowWhenNotDileveredSuccessfully(delivered, rq.Uri, rq.Method);
    }

    public async Task<TRs> ForceSendAsync<TRs>(
        HttpRq rq,
        CancellationToken ct = default)
    {
        var (delivered, rs) = await SendAsync<TRs>(rq, ct);
        HttpRqNotDeliveriedException.ThrowWhenNotDileveredSuccessfully(delivered, rq.Uri, rq.Method);
        return rs!;
    }

    public async Task<TRs> ForceSendAsync<TBody, TRs>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default)
    {
        var (delivered, rs) = await SendAsync<TBody, TRs>(rq, ct);
        HttpRqNotDeliveriedException.ThrowWhenNotDileveredSuccessfully(delivered, rq.Uri, rq.Method);
        return rs!;
    }

    public async Task<bool> SendAsync(
        HttpRq rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        var delivered = httpResponseMessageToBoolMapper.Map(rs);
        return delivered;
    }

    public async Task<bool> SendAsync<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpWithBodyRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        var delivered = httpResponseMessageToBoolMapper.Map(rs);
        return delivered;
    }

    public async Task<(bool Delivered, TRs? Rs)> SendAsync<TRs>(
        HttpRq rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        var result = await httpResponseMessageToDeliveredRsMapper.MapAsync<TRs>(rs, ct);
        return result;
    }

    public async Task<(bool Delivered, TRs? Rs)> SendAsync<TBody, TRs>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpWithBodyRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        var result = await httpResponseMessageToDeliveredRsMapper.MapAsync<TRs>(rs, ct);
        return result;
    }

    public async Task<(bool Delivered, Stream? Rs)> SendAsyncStream<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpWithBodyRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        var result = await httpResponseMessageToStreamRsMapper.MapAsync(rs, ct);
        return result;
    }

    public async Task<bool> SendFullRqAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default)
    {
        var rs = await pollyClient.SendAsync(rq, ct);
        var delivered = httpResponseMessageToBoolMapper.Map(rs);
        return delivered;
    }

    public async Task<(bool Delivered, TRs? Rs)> SendFullRqAsync<TRs>(
        HttpRequestMessage rq,
        CancellationToken ct = default)
    {
        var rs = await pollyClient.SendAsync(rq, ct);
        var result = await httpResponseMessageToDeliveredRsMapper.MapAsync<TRs>(rs, ct);
        return result;
    }

    public async Task<HttpResponseMessage> SendFullRsAsync(
        HttpRq rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        return rs;
    }

    public async Task<HttpResponseMessage> SendFullRsAsync<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default)
    {
        var rqMessage = httpWithBodyRqToMessageMapper.Map(rq);
        var rs = await pollyClient.SendAsync(rqMessage, ct);
        return rs;
    }

    public Task<HttpResponseMessage> SendFullRqRsAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default)
        => pollyClient.SendAsync(rq, ct);

    public async Task<TRs> SendFormAsync<TRs>(
        Uri requestUri,
        FormUrlEncodedContent content, 
        CancellationToken ct = default)
    {
        var rs = await pollyClient.SendFormAsync(requestUri, content, ct);
        var (Delivered, Rs) = await httpResponseMessageToDeliveredRsMapper.MapAsync<TRs>(rs, ct);
        HttpRqNotDeliveriedException.ThrowWhenNotDileveredSuccessfully(Delivered, requestUri, HttpMethod.Post);
        return Rs!;
    }

    public async Task<TRs> SendFormAsync<TRs>(
        Uri requestUri, 
        MultipartFormDataContent form, 
        CancellationToken ct = default)
    {
        var rs = await pollyClient.SendFormAsync(requestUri, form, ct);
        var (Delivered, Rs) = await httpResponseMessageToDeliveredRsMapper.MapAsync<TRs>(rs, ct);
        HttpRqNotDeliveriedException.ThrowWhenNotDileveredSuccessfully(Delivered, requestUri, HttpMethod.Post);
        return Rs!;
    }
}
