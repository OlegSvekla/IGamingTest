using IGamingTest.Core.Startups;
using IGamingTest.Infrastructure.DependencyInjection;

namespace IGamingTest.Web.Startups.Helpers;

public sealed class AppBuilder
{
    private readonly IList<ICustomStartup> startups = [];
    private readonly IDependencyResolver resolver;

    private AppBuilder(
        IDependencyResolver resolver)
        => this.resolver = resolver;

    public static AppBuilder New(
        IDependencyResolver resolver)
        => new AppBuilder(resolver);

    public AppBuilder With<TStartup>()
        where TStartup : ICustomStartup, new()
    {
        var startup = resolver.Resolve<TStartup>();
        startups.Add(startup);
        return this;
    }

    public async Task<WebApplication> BuildAsync(WebApplicationBuilder builder)
    {
        var appExecutor = new AppExecutor(startups);
        return await appExecutor.ExecuteAsync(builder);
    }
}
