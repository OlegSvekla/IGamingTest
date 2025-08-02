using IGamingTest.Core.Http.Out.Models;

namespace IGamingTest.Core.Http.Out;

/// <summary>
/// Http sender with portion of ready to use patterns and strategies
/// </summary>
public interface IHttpSender
{
    /// <summary>
    /// Force send request means response status codes <200 and >300 throw exception
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task ForceSendAsync(
        HttpRq rq,
        CancellationToken ct = default);

    /// <summary>
    /// Force send request means response status codes <200 and >300 throw exception
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task ForceSendAsync<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default);

    /// <summary>
    /// Force send request means response status codes <200 and >300 throw exception
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TRs> ForceSendAsync<TRs>(
        HttpRq rq,
        CancellationToken ct = default);

    /// <summary>
    /// Post - work with x-www-formurlencoded protocole.
    /// </summary>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="content"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TRs> SendFormAsync<TRs>(
        Uri requestUri,
        FormUrlEncodedContent content,
        CancellationToken ct = default);

    /// <summary>
    /// Post - work with multipart/form-data protocole.
    /// </summary>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="content"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TRs> SendFormAsync<TRs>(
        Uri requestUri,
        MultipartFormDataContent form,
        CancellationToken ct = default);

    /// <summary>
    /// Force send request means response status codes <200 and >300 throw exception
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TRs> ForceSendAsync<TBody, TRs>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status</returns>
    Task<bool> SendAsync(
        HttpRq rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status</returns>
    Task<bool> SendAsync<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status and Response</returns>
    Task<(bool Delivered, TRs? Rs)> SendAsync<TRs>(
        HttpRq rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status and Response</returns>
    Task<(bool Delivered, TRs? Rs)> SendAsync<TBody, TRs>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status and Stream</returns>
    Task<(bool Delivered, Stream? Rs)> SendAsyncStream<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully and return original http response message
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status</returns>
    Task<bool> SendFullRqAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request means response status codes <200 and >300 will be marked as not delivered
    /// otherwice request has been delivered successfully and return original http response message
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <typeparam name="TRs"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns>Delivery status and Response</returns>
    Task<(bool Delivered, TRs? Rs)> SendFullRqAsync<TRs>(
        HttpRequestMessage rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request and return original http response message
    /// </summary>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> SendFullRsAsync(
        HttpRq rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send request and return original http response message
    /// </summary>
    /// <typeparam name="TBody"></typeparam>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> SendFullRsAsync<TBody>(
        HttpWithBodyRq<TBody> rq,
        CancellationToken ct = default);

    /// <summary>
    /// Send original request message and return original http response message
    /// </summary>
    /// <param name="rq"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<HttpResponseMessage> SendFullRqRsAsync(
        HttpRequestMessage rq,
        CancellationToken ct = default);
}
