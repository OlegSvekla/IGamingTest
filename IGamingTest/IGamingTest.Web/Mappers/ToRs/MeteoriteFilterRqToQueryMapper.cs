using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Mappers;
using IGamingTest.Web.Rqs;

namespace IGamingTest.Web.Mappers.ToRs;

public class MeteoriteFilterRqToQueryMapper
: IMapper<GetMeteoriteFilterRq, GetMeteoriteFilterQuery>
{
public GetMeteoriteFilterQuery Map(
    GetMeteoriteFilterRq input)
    => new GetMeteoriteFilterQuery
    (
        input.YearFrom,
        input.YearTo,
        input.RecClass,
        input.NameContains,
        input.Amount,
        input.Offset,
        input.SortBy,
        input.SortDirection
    );
}