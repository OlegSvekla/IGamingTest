using IGamingTest.Core.Polly.Providers;
using Polly;
using System.Threading.RateLimiting;

namespace IGamingTest.Core.Polly.Clients.Policies;

public interface IHttpRateLimiterPolicy : IPolicySetup;

public class HttpRateLimiterPolicy(
    IHttpConfigProvider configProvider
    ) : IHttpRateLimiterPolicy
{
    public PipelineOrderEnum PipelineOrder => PipelineOrderEnum.RateLimiter;

    public ResiliencePipelineBuilder<HttpResponseMessage> SetPolicy(
        ResiliencePipelineBuilder<HttpResponseMessage> pb)
    {
        var rateLimiterConfig = configProvider.GetRateLimiterPolicyConfig();

        var tokenLimit = rateLimiterConfig.TokenLimit;
        var queueLimit = rateLimiterConfig.QueueLimit;
        var timeSpan = TimeSpan.FromSeconds(rateLimiterConfig.ReplenishmentPeriod);
        var tokensPerPeriod = rateLimiterConfig.TokensPerPeriod;
        var autoReplenishment = rateLimiterConfig.AutoReplenishment;

        var options = new TokenBucketRateLimiterOptions
        {
            TokenLimit = tokenLimit,
            QueueLimit = queueLimit,
            ReplenishmentPeriod = timeSpan,
            TokensPerPeriod = tokensPerPeriod,
            AutoReplenishment = autoReplenishment
        };
        var limiter = new TokenBucketRateLimiter(options);

        pb.AddRateLimiter(limiter);
        return pb;
    }
}
