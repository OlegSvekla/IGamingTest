using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using IGamingTest.Core.Urls.Configs;
using IGamingTest.Core.Urls.Providers;

namespace IGamingTest.Web.Startups;

public sealed class UrlStartup : ServiceStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.Cors;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.Configure<UrlConfig>(builder.Configuration.GetSection(Core.Urls.Consts.WebUrlConfigSectionKey));
        services.AddSingleton<IUrlProvider, UrlProvider>();

        return ValueTask.FromResult(services);
    }
}
