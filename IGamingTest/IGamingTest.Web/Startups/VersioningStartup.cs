using Asp.Versioning;
using IGamingTest.Core.Startups;

namespace IGamingTest.Web.Startups;

/// <summary>
/// Microservice data.
/// Custom implementation to get basic information about executed microservice
/// </summary>
public sealed class VersioningStartup : ServiceStartup
{
    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services
            .AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return ValueTask.FromResult(services);
    }
}
