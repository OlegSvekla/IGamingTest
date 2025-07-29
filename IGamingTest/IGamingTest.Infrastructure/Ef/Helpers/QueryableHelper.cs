using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IGamingTest.Infrastructure.Ef.Helpers;

public static class QueryableHelper
{
    public static IQueryable<TEntity> Paginate<TEntity>(
        this IQueryable<TEntity> value,
        int? amount = null,
        int? offset = null)
    {
        var selectedAmount = amount ?? Consts.MaxAmount;
        var selectedOffset = offset ?? Consts.MinOffset;

        selectedAmount = Consts.MinAmount <= selectedAmount && selectedAmount <= Consts.MaxAmount
            ? selectedAmount
            : Consts.MinAmount;
        selectedOffset = Consts.MinOffset <= selectedOffset && selectedOffset <= Consts.MaxOffset
            ? selectedOffset
            : Consts.MinOffset;

        return value
            .Skip(selectedOffset)
            .Take(selectedAmount);
    }

    public static IQueryable<TEntity> Filter<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, bool>>? predicate = null)
        => predicate != null
            ? source.Where(predicate)
            : source;

    public static IQueryable<TEntity> Sort<TEntity>(
        this IQueryable<TEntity> source,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null)
        => sorter != null
            ? sorter(source)
            : source;

    public static Task<int> RunCountAsync<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken ct = default)
        => predicate != null
            ? source.CountAsync(predicate, ct)
            : source.CountAsync(ct);
}
