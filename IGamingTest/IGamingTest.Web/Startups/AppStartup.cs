using IGamingTest.Core.Startups;

namespace IGamingTest.Web.Startups;

public interface IAppStartup : ICustomStartup
{
}

public abstract class AppStartup
    : BaseStartup, IAppStartup
{
    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder appBuilder)
        => ValueTask.FromResult(services);
}
