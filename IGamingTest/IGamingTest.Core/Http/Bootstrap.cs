using IGamingTest.Core.Http.In;
using IGamingTest.Core.Http.Out;

namespace IGamingTest.Core.Http;

public static class Bootstrap
{
    public static IServiceCollection AddBatchHttp(
        this IServiceCollection services)
    {
        services.AddBatchHttpIn();
        services.AddBatchHttpOut();

        return services;
    }
}
