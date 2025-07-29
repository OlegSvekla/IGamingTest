using IGamingTest.BL;
using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using IGamingTest.Infrastructure;
using IGamingTest.Web.Mappers;

namespace IGamingTest.Web.Startups;

internal sealed class Startup : ServiceStartup
{
    public override ServiceRegistrationOrderEnum ServiceRegistrationOrder
        => ServiceRegistrationOrderEnum.Highest;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddWebMappers();
        services.AddStartupInfrastructure(builder.Configuration);
        services.AddStartupIGamingTestBlServices();

        builder.Services.AddHttpClient();

        return ValueTask.FromResult(services);
    }
}
