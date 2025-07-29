using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using IGamingTest.Web.Startups.Helpers;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace IGamingTest.Web.Startups;

/// <summary>
/// Swagger.
/// It provides ability to see exist endpoints.
/// See <see cref="https://aka.ms/aspnetcore/swashbuckle"/>
/// </summary>
public sealed class SwaggerStartup : BaseStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.PreEndpoint;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder appBuilder)
    {
        var appVersion = appBuilder.Configuration[Consts.AppVersionConfigSectionKey] ?? "1.0.0";
        var appTitle = Assembly.GetEntryAssembly()?.GetName().Name ?? "Default API";

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = appTitle,
                Version = $"{appBuilder.Environment.EnvironmentName}-{appVersion}"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

            options.SchemaFilter<EnumSchemaFilter>();
            options.OperationFilter<EnumOperationFilter>();
        });

        return ValueTask.FromResult(services);
    }

    public override ValueTask<WebApplication> UseAsync(
        WebApplication app)
    {
        if (app.Environment.IsDevelopment()
            || app.Environment.IsEnvironment("tst")
            || app.Environment.IsEnvironment("docker"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return ValueTask.FromResult(app);
    }
}
