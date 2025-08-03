namespace IGamingTest.Infrastructure.Polly.Configs;

public class HttpConfig
{
    public TimeoutConfig TimeoutPolicy { get; init; } = default!;

    public RetryConfig RetryPolicy { get; init; } = default!;

    public RateLimiterConfig RateLimiterPolicy { get; init; } = default!;
}
