using Polly;

namespace IGamingTest.Core.Polly.Clients.Policies;

public interface IPolicySetup
{
    PipelineOrderEnum PipelineOrder { get; }

    ResiliencePipelineBuilder<HttpResponseMessage> SetPolicy(
        ResiliencePipelineBuilder<HttpResponseMessage> pb);
}
