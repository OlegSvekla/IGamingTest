using IGamingTest.Core.Cors.Configs;
using IGamingTest.Core.Cors.Providers;
using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using Microsoft.Extensions.Options;

namespace IGamingTest.Web.Startups;

/// <summary>
/// Cors.
/// Cross Origin Resource Sharing is necessary for browser security which prevents a web
/// page from making requests to a different domain than the one that served the web page.
/// See <see cref="https://learn.microsoft.com/en-us/aspnet/core/security/cors"/>
/// </summary>
public sealed class CorsStartup : BaseStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.Cors;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.Configure<CorsConfig>(builder.Configuration.GetSection(Core.Cors.Consts.WebCorsConfigSectionKey));
        services.AddSingleton<ICorsProvider, CorsProvider>();

        return ValueTask.FromResult(services);
    }

    public override ValueTask<WebApplication> UseAsync(
        WebApplication app)
    {
        var corsConfig = app.Services.GetRequiredService<IOptions<CorsConfig>>().Value;

        app.UseCors(builder => builder
            .WithOrigins(corsConfig.AllowedOrigins)
            .AllowAnyHeader()
            .AllowCredentials()
            .WithMethods(corsConfig.AllowedMethods));

        return ValueTask.FromResult(app);
    }
}
