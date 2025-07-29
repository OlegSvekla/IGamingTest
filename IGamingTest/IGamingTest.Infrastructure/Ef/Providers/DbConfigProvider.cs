using IGamingTest.Ef.Configs;
using Microsoft.Extensions.Options;

namespace IGamingTest.Ef.Providers;

public interface IDbConfigProvider
{
    DbConfig GetDbConfig();
}

public sealed class DbConfigProvider(
    IOptions<DbConfig> options
    ) : IDbConfigProvider
{
    private readonly DbConfig config = options.Value;

    public DbConfig GetDbConfig()
        => config;
}
