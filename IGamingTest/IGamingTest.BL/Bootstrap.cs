using IGamingTest.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.BL;

public static class Bootstrap
{
    public static void AddBlServices(this IServiceCollection services)
    {
        services.AddScoped<IMeteoriteService, MeteoriteService>();
    }
}
