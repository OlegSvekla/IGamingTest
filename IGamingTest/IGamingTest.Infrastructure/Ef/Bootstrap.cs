using IGamingTest.Infrastructure.Ef.Repositories;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.Infrastructure.Ef;

public static class Bootstrap
{
    public static IServiceCollection AddStartupRepository(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUow, Uow>();
        services.AddDatabase(configuration);
        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<GameContext>(options =>
            options.UseNpgsql(configuration.GetSection("Database:ConnectionString").Value));
}
