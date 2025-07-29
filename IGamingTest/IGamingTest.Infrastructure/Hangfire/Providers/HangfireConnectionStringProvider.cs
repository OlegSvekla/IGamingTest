using IGamingTest.Infrastructure.Hangfire.Configs;
using Microsoft.Extensions.Options;

namespace IGamingTest.Infrastructure.Hangfire.Providers;

public interface IHangfireConnectionStringProvider
{
    string GetConnectionString();
}

public sealed class HangfireConnectionStringProvider(
    IOptions<HangfireConfig> options) : IHangfireConnectionStringProvider
{
    private readonly HangfireConfig config = options.Value;

    public string GetConnectionString()
        => config.ConnectionString;
}
