using Hangfire;

namespace IGamingTest.Infrastructure.Hangfire.Providers;

public interface IDbConnectionProvider
{
    IGlobalConfiguration AddDbConnection(IGlobalConfiguration config);
}
