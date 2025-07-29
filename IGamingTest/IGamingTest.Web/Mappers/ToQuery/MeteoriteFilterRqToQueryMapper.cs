using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Mappers;
using IGamingTest.Web.Rqs;

namespace IGamingTest.Web.Mappers.ToQuery;

public class MeteoriteFilterRqToQueryMapper
: IMapper<MeteoriteFilterRq, MeteoriteFilterQuery>
{
    public MeteoriteFilterQuery Map(
        MeteoriteFilterRq input)
        => new MeteoriteFilterQuery
        {
            YearFrom = input.YearFrom,
            YearTo = input.YearTo,
            RecClass = input.RecClass,
            NameContains = input.NameContains,
            Amount = input.Amount,
            Offset = input.Offset,
            SortBy = input.SortBy
        };
}
