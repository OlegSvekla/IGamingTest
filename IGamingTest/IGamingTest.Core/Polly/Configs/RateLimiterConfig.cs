namespace IGamingTest.Core.Polly.Configs;

public class RateLimiterConfig
{
    public int TokenLimit { get; init; }

    public int QueueLimit { get; init; }

    public int ReplenishmentPeriod { get; init; }

    public int TokensPerPeriod { get; init; }

    public bool AutoReplenishment { get; init; }
}
