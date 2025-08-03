using IGamingTest.Core.Polly.Providers;
using Polly;
using Polly.Timeout;

namespace IGamingTest.Infrastructure.Polly.Clients.Policies;

public interface IHttpOneRqTimeoutPolicy : IPolicySetup;

public class HttpOneRqTimeoutPolicy(
    IHttpConfigProvider configProvider
    ) : IHttpOneRqTimeoutPolicy
{
    public PipelineOrderEnum PipelineOrder => PipelineOrderEnum.OneHttpRqTimeout;

    public ResiliencePipelineBuilder<HttpResponseMessage> SetPolicy(
        ResiliencePipelineBuilder<HttpResponseMessage> pb)
    {
        var timeoutConfig = configProvider.GetTimeoutPolicyConfig();

        var timeout = TimeSpan.FromSeconds(timeoutConfig.TimeoutSec);
        var options = new TimeoutStrategyOptions
        {
            Timeout = timeout
        };

        pb.AddTimeout(options);
        return pb;
    }
}
