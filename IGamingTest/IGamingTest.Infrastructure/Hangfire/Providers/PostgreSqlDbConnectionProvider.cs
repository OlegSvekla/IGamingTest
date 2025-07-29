using Hangfire;
using Hangfire.PostgreSql;

namespace IGamingTest.Infrastructure.Hangfire.Providers;

public sealed class PostgreSqlDbConnectionProvider(
    IHangfireConnectionStringProvider connectionStringProvider
    ) : IDbConnectionProvider
{
    public IGlobalConfiguration AddDbConnection(IGlobalConfiguration config)
    => config.UsePostgreSqlStorage(options
            => options.UseNpgsqlConnection(connectionStringProvider.GetConnectionString()));
}
