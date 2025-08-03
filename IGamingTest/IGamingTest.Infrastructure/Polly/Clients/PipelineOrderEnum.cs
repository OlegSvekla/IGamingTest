namespace IGamingTest.Infrastructure.Polly.Clients;

public enum PipelineOrderEnum
{
    OneHttpRqTimeout,
    RateLimiter,
    Retry
}
