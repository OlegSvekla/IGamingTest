using IGamingTest.Core.Http.Polly;
using IGamingTest.Core.Startups;

namespace IGamingTest.Web.Startups;

public class HttpPollyStartup : ServiceStartup
{
    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddBatchHttpPolly(builder.Configuration);

        return ValueTask.FromResult(services);
    }
}
