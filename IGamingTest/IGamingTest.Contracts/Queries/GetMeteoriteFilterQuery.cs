using IGamingTest.Core.Models;
using IGamingTest.Infrastructure.Mq.Messages;

namespace IGamingTest.Contracts.Queries;

public sealed record GetMeteoriteFilterQuery(
    int? YearFrom,
    int? YearTo,
    string? RecClass,
    string? NameContains,
    int? Amount,
    int? Offset,
    string? SortBy,
    string? SortDirection
    ) : IMediatrQuery<IReadOnlyCollection<GetMeteoriteFilterQueryRs>>;
