using IGamingTest.Infrastructure.Hangfire.Configs;
using Microsoft.Extensions.Options;

namespace IGamingTest.Infrastructure.Hangfire.Providers;

public interface IHangfireWorkerCountProvider
{
    int GetWorkerCount();
}

public sealed class HangfireWorkerCountProvider(
    IOptions<HangfireConfig> options) : IHangfireWorkerCountProvider
{
    private readonly HangfireConfig config = options.Value;

    public int GetWorkerCount()
        => config.WorkerCount;
}
