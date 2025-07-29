using IGamingTest.Infrastructure.Ef;
using IGamingTest.Infrastructure.Ef.Repositories;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using IGamingTest.Infrastructure.Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddStartupInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddStartupRepository(configuration);
        return services;
    }
}
