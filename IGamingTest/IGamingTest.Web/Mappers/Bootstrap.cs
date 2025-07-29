using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Mappers;
using IGamingTest.Web.Mappers.ToQuery;
using IGamingTest.Web.Rqs;

namespace IGamingTest.Web.Mappers;

public static class Bootstrap
{
    public static IServiceCollection AddWebMappers(this IServiceCollection services)
    {
        services.AddScoped<IMapper<MeteoriteFilterRq, MeteoriteFilterQuery>, MeteoriteFilterRqToQueryMapper>();

        return services;
    }
}
