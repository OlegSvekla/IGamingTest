using IGamingTest.Core.Http;
using IGamingTest.Core.Startups;

namespace IGamingTest.Web.Startups;

public class HttpStartup : ServiceStartup
{
    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddBatchHttp();

        return ValueTask.FromResult(services);
    }
}
