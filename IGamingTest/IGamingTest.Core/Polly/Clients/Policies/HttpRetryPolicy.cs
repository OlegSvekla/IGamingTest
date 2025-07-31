using IGamingTest.Core.Polly.Providers;
using IGamingTest.Infrastructure.Ef.Helpers;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System.Net;

namespace IGamingTest.Core.Polly.Clients.Policies;

public interface IHttpRetryPolicy : IPolicySetup;

public class HttpRetryPolicy(
    IHttpConfigProvider configProvider
    ) : IHttpRetryPolicy
{
    private static readonly IReadOnlyCollection<Type> RetryingExs =
    [
        typeof(HttpRequestException),
        typeof(TimeoutRejectedException)
    ];

    private static readonly IReadOnlyCollection<HttpStatusCode> UnretryingHttpCodes =
    [
        HttpStatusCode.Unauthorized,
        HttpStatusCode.RequestTimeout,
        //HttpStatusCode.TooManyRequests,   //TODO: Verify, do we need it. We already have delay between calls
        HttpStatusCode.InternalServerError,
        HttpStatusCode.BadGateway,
        HttpStatusCode.ServiceUnavailable,
        HttpStatusCode.GatewayTimeout
    ];

    public PipelineOrderEnum PipelineOrder => PipelineOrderEnum.Retry;

    public ResiliencePipelineBuilder<HttpResponseMessage> SetPolicy(
        ResiliencePipelineBuilder<HttpResponseMessage> pb)
    {
        var retryConfig = configProvider.GetRetryPolicyConfig();

        var maxRetryAttempts = retryConfig.MaxRetryAttemptsCount;
        var delay = TimeSpan.FromSeconds(retryConfig.DelaySec);
        var options = new RetryStrategyOptions<HttpResponseMessage>
        {
            MaxRetryAttempts = maxRetryAttempts,
            Delay = delay,
            ShouldHandle = ShouldRetryAsync
        };

        pb.AddRetry(options);
        return pb;
    }

    protected virtual async ValueTask<bool> ShouldRetryAsync(
        RetryPredicateArguments<HttpResponseMessage> args)
        => args.Outcome.Exception is not null
            ? await ShouldRetryOnExceptionAsync(
                ex: args.Outcome.Exception,
                ct: args.Context.CancellationToken)
            : await ShouldRetryOnResponseAsync(
                rs: args.Outcome.Result.GetValue(),
                ct: args.Context.CancellationToken);

    protected virtual ValueTask<bool> ShouldRetryOnExceptionAsync(
        Exception ex,
        CancellationToken ct)
        => ValueTask.FromResult(
            RetryingExs.Contains(ex.GetType()));

    protected virtual ValueTask<bool> ShouldRetryOnResponseAsync(
        HttpResponseMessage rs,
        CancellationToken ct)
        => ValueTask.FromResult(
            !rs.IsSuccessStatusCode
            && !UnretryingHttpCodes.Contains(rs.StatusCode));
}
