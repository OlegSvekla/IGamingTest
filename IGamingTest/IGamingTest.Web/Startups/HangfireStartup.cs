using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using IGamingTest.Infrastructure.Hangfire;

namespace IGamingTest.Web.Startups;

public sealed class HangfireStartup : BaseStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.PreEndpoint;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddBatchJobsHangfire(builder.Configuration);

        return ValueTask.FromResult(services);
    }

    public override async ValueTask<WebApplication> UseAsync(
        WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        await app.UseJobsHangfire();

        JobsRegistrator.StartupJobs(scope);

        return app;
    }
}
