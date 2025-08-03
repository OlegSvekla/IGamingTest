namespace IGamingTest.Infrastructure.Polly.Configs;

public class RetryConfig
{
    public int MaxRetryAttemptsCount { get; init; }

    public int DelaySec { get; init; }
}
