using IGamingTest.Core.Http.Out;
using IGamingTest.Core.Polly.Providers;
using IGamingTest.Infrastructure.Polly.Clients;
using IGamingTest.Infrastructure.Polly.Clients.Policies;
using IGamingTest.Infrastructure.Polly.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.Infrastructure.Http.Polly;

public static class Bootstrap
{
    public static IServiceCollection AddBatchHttpPolly(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddAppSettingsHttpPolly(config);
        services.AddCoreHttpPolly();

        return services;
    }

    public static IServiceCollection AddCoreHttpPolly(
        this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<IHttpSender, PollySender>();
        services.AddSingleton<HttpOneRqTimeoutPolicy>();
        services.AddSingleton<IPolicySetup, HttpOneRqTimeoutPolicy>(sp => sp.GetRequiredService<HttpOneRqTimeoutPolicy>());
        services.AddSingleton<IHttpOneRqTimeoutPolicy, HttpOneRqTimeoutPolicy>(sp => sp.GetRequiredService<HttpOneRqTimeoutPolicy>());
        services.AddSingleton<HttpRateLimiterPolicy>();
        services.AddSingleton<IPolicySetup, HttpRateLimiterPolicy>(sp => sp.GetRequiredService<HttpRateLimiterPolicy>());
        services.AddSingleton<IHttpRateLimiterPolicy, HttpRateLimiterPolicy>(sp => sp.GetRequiredService<HttpRateLimiterPolicy>());
        services.AddSingleton<HttpRetryPolicy>();
        services.AddSingleton<IPolicySetup, HttpRetryPolicy>(sp => sp.GetRequiredService<HttpRetryPolicy>());
        services.AddSingleton<IHttpRetryPolicy, HttpRetryPolicy>(sp => sp.GetRequiredService<HttpRetryPolicy>());
        services.AddScoped<IPollyClient, PollyClient>();

        return services;
    }

    public static IServiceCollection AddAppSettingsHttpPolly(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<HttpConfig>(config.GetSection(Consts.HttpConfigSectionKey));
        services.AddSingleton<IHttpConfigProvider, HttpConfigProvider>();

        return services;
    }
}
