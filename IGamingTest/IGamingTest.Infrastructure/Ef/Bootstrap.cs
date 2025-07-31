using IGamingTest.Ef.Configs;
using IGamingTest.Ef.Providers;
using IGamingTest.Infrastructure.Ef.Repositories;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using IGamingTest.Infrastructure.Ef.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.Infrastructure.Ef;

public static class Bootstrap
{
    public static IServiceCollection AddBatchEf(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddAppSettingsEf(config);
        services.AddCoreEf();

        return services;
    }

    public static IServiceCollection AddCoreEf(
        this IServiceCollection services)
    {
        services.AddScoped<IUow, Uow>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IDbConnector, DbConnector>();
        services.AddScoped<IDbSeeder, DbSeeder>();

        return services;
    }

    public static IServiceCollection AddAppSettingsEf(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<DbConfig>(config.GetSection(Consts.DbConfigSectionKey));
        services.AddSingleton<IDbConfigProvider, DbConfigProvider>();

        return services;
    }

    public static async ValueTask UseEfAsync(
        this IServiceProvider sp,
        CancellationToken ct)
    {
        var dbContext = sp.GetRequiredService<IEfContext>();
        await dbContext.InitAsync(ct);
    }
}
