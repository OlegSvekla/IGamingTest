using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Entities.Meteorite;
using IGamingTest.Core.Models;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using IGamingTest.Infrastructure.Mq.Handlers;
using System.Linq.Expressions;

namespace IGamingTest.BL.Handler;

internal class GetMeteoritesQueryHandler(
    IUow uow
    ) : MediatrQueryHandler<GetMeteoriteFilterQuery, IReadOnlyCollection<GetMeteoriteFilterQueryRs>>
{
    public override async ValueTask<IReadOnlyCollection<GetMeteoriteFilterQueryRs>> HandleAsync(
        GetMeteoriteFilterQuery query,
        CancellationToken ct)
    {
          var filter = BuildFilterExpression(query);

          var grouped = await uow.MeteoriteRepository.GetAllGroupedAsync(
              groupSelector: g => new GetMeteoriteFilterQueryRs
              (
                  g.Key,
                  g.Count(),
                  g.Sum(x => x.Mass)
              ),
              groupBy: e => e.Year,
              amount: query.Amount,
              offset: query.Offset,
              predicate: filter,
              ct: ct);

          var sorted = BuildGroupedSorter(query.SortBy, query.SortDirection)(grouped);

          return sorted.ToList();
    }

    private Expression<Func<MeteoriteEntity, bool>> BuildFilterExpression(GetMeteoriteFilterQuery rq)
    {
        return e =>
            (!rq.YearFrom.HasValue || e.Year >= rq.YearFrom.Value) &&
            (!rq.YearTo.HasValue || e.Year <= rq.YearTo.Value) &&
            (string.IsNullOrEmpty(rq.RecClass) || e.RecClass == rq.RecClass) &&
            (string.IsNullOrEmpty(rq.NameContains) || (e.Name != null && e.Name.Contains(rq.NameContains)));
    }

    private Func<IEnumerable<GetMeteoriteFilterQueryRs>, IOrderedEnumerable<GetMeteoriteFilterQueryRs>> BuildGroupedSorter(
        string? sortBy,
        string? sortDirection)
    {
        var desc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);
        var prop = sortBy?.ToLower() ?? "year";

        return q => prop switch
        {
            "count" => desc ? q.OrderByDescending(x => x.Count) : q.OrderBy(x => x.Count),
            "mass" => desc ? q.OrderByDescending(x => x.TotalMass) : q.OrderBy(x => x.TotalMass),
            "year" or _ => desc ? q.OrderByDescending(x => x.Year) : q.OrderBy(x => x.Year)
        };
    }
}
