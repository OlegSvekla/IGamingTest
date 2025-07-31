namespace IGamingTest.Core.Polly.Clients;

public enum PipelineOrderEnum
{
    OneHttpRqTimeout,
    RateLimiter,
    Retry
}
