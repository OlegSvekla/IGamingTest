using IGamingTest.Core.Mappers;
using IGamingTest.Core.Models;
using IGamingTest.Web.Rss;

namespace IGamingTest.Web.Mappers.ToRs;

public class MeteoriteFilterQueryRsToRsMapper
: IMapper<IReadOnlyCollection<GetMeteoriteFilterQueryRs>, IReadOnlyCollection<GetMeteoriteFilterRs>>
{
    public IReadOnlyCollection<GetMeteoriteFilterRs> Map(IReadOnlyCollection<GetMeteoriteFilterQueryRs> input)
        => input.Select(x => new GetMeteoriteFilterRs(
            x.Year,
            x.Count,
            x.TotalMass
        )).ToList();
}
