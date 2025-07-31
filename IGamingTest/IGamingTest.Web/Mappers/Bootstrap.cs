using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Mappers;
using IGamingTest.Core.Models;
using IGamingTest.Web.Mappers.ToQuery;
using IGamingTest.Web.Mappers.ToRs;
using IGamingTest.Web.Rqs;
using IGamingTest.Web.Rss;

namespace IGamingTest.Web.Mappers;

public static class Bootstrap
{
    public static IServiceCollection AddWebMappers(this IServiceCollection services)
    {
        services.AddScoped<IMapper<GetMeteoriteFilterRq, GetMeteoriteFilterQuery>, MeteoriteFilterRqToQueryMapper>();
        services.AddScoped<IMapper<IReadOnlyCollection<GetMeteoriteFilterQueryRs>, IReadOnlyCollection<GetMeteoriteFilterRs>>, MeteoriteFilterQueryRsToRsMapper>();

        return services;
    }
}
