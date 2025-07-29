using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;

namespace IGamingTest.Web.Startups;

internal sealed class UseJobsStartup : ServiceStartup
{
    public override ServiceRegistrationOrderEnum ServiceRegistrationOrder
        => ServiceRegistrationOrderEnum.Highest;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        using var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        //await app.UseJobsHangfire(scope);

        JobsRegistrator.StartupJobs(scope);

        return ValueTask.FromResult(services);
    }
}
