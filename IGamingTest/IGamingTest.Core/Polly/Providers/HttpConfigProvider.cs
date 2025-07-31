using IGamingTest.Core.Polly.Configs;
using Microsoft.Extensions.Options;

namespace IGamingTest.Core.Polly.Providers;

public interface IHttpConfigProvider
{
    TimeoutConfig GetTimeoutPolicyConfig();

    RetryConfig GetRetryPolicyConfig();

    RateLimiterConfig GetRateLimiterPolicyConfig();
}

public class HttpConfigProvider(
    IOptions<HttpConfig> options
    ) : IHttpConfigProvider
{
    private readonly HttpConfig config = options.Value;

    public TimeoutConfig GetTimeoutPolicyConfig()
        => config.TimeoutPolicy;

    public RetryConfig GetRetryPolicyConfig()
        => config.RetryPolicy;

    public RateLimiterConfig GetRateLimiterPolicyConfig()
        => config.RateLimiterPolicy;
}
