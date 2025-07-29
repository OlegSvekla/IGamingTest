using IGamingTest.Core.Interfaces.Entities;
using IGamingTest.Ef.Exceptions;
using IGamingTest.Infrastructure.Ef.Helpers;
using IGamingTest.Infrastructure.Ef.Repositories.Enums;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace IGamingTest.Infrastructure.Ef.Repositories;

public class Repository<TEntity, TId>(
    GameContext context
    ) : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>, new()
    where TId : IEquatable<TId>
{
    #region Get/Find one entity

    /// <summary>
    /// Find entity by id otherwise returns null
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<TEntity?> FindByIdAsync(
        TId id,
        Expression<Func<TEntity, TEntity>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
        => FindAsync(
            mode: SearchModeEnum.Single,
            predicate: x => id.Equals(x.Id),
            selector: selector,
            behavior: behavior,
            ct: ct);

    /// <summary>
    /// Find entity by id otherwise returns null
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<TResult?> FindByIdAsync<TResult>(
        TId id,
        Expression<Func<TEntity, TResult>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
        => FindAsync(
            mode: SearchModeEnum.Single,
            predicate: x => id.Equals(x.Id),
            selector: selector,
            behavior: behavior,
            ct: ct);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<TEntity> GetByIdAsync(
        TId id,
        Expression<Func<TEntity, TEntity>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default)
        => GetAsync(
            mode: SearchModeEnum.Single,
            predicate: x => id.Equals(x.Id),
            selector: selector,
            behavior: behavior,
            errorMessageWhenNotFound: errorMessageWhenNotFound,
            ct: ct);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<TResult> GetByIdAsync<TResult>(
        TId id,
        Expression<Func<TEntity, TResult>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default)
        where TResult : class
        => GetAsync(
            mode: SearchModeEnum.Single,
            predicate: x => id.Equals(x.Id),
            selector: selector,
            behavior: behavior,
            errorMessageWhenNotFound: errorMessageWhenNotFound,
            ct: ct);

    /// <summary>
    /// Find entity by predicate otherwise returns null
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<TEntity?> FindAsync(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
        => FindAsync<TEntity>(
            mode: mode,
            predicate: predicate,
            selector: selector,
            sorter: sorter,
            behavior: behavior,
            ct: ct);

    /// <summary>
    /// Find entity by predicate otherwise returns null
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<TResult?> FindAsync<TResult>(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
    {
        try
        {
            Func<IQueryable<TResult>, CancellationToken, Task<TResult?>> findAsync = mode switch
            {
                SearchModeEnum.Single => EntityFrameworkQueryableExtensions.SingleOrDefaultAsync,
                SearchModeEnum.First => EntityFrameworkQueryableExtensions.FirstOrDefaultAsync,
                SearchModeEnum.Last => EntityFrameworkQueryableExtensions.LastOrDefaultAsync,
                _ => throw new InvalidOperationException()
            };

            var searchResult = Search(
                selector: selector,
                predicate: predicate,
                sorter: sorter,
                behavior: behavior);
            return await findAsync(searchResult, ct);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<TEntity> GetAsync(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default)
        => GetAsync<TEntity>(
            mode: mode,
            predicate: predicate,
            selector: selector,
            sorter: sorter,
            behavior: behavior,
            errorMessageWhenNotFound: errorMessageWhenNotFound,
            ct: ct);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<TResult> GetAsync<TResult>(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default)
        where TResult : class
    {
        var result = await FindAsync(mode, predicate, selector, sorter, behavior, ct);
        EntityNotFoundException.ThrowIfNull(result, typeof(TResult).Name, errorMessageWhenNotFound);
        return result;
    }

    /// <summary>
    /// Whether entity exist by Id or not
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>HEADER
    /// <returns></returns>
    public Task<bool> ExistByIdAsync(
        TId id,
        CancellationToken ct = default)
        => ExistByPropertyAsync(
            predicate: x => id.Equals(x.Id),
            ct: ct);

    /// <summary>
    /// Whether entity exist by Id or not
    /// </summary>
    /// <param name="TEntity"></param>
    /// <returns></returns>
    public Task<bool> ExistByPropertyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
        => context.Set<TEntity>()
            .AnyAsync(predicate, ct);

    public Task<int> CountAsync(
        int amount = int.MaxValue,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        CancellationToken ct = default)
        => context.Set<TEntity>()
            .Sort(sorter)
            .Paginate(amount, offset)
            .RunCountAsync(predicate, ct);

    #endregion Get/Find one entity

    #region Get/Find multiple entity

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    public Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, TEntity>> selector,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
        => GetAllAsync<TEntity>(
            selector,
            amount,
            offset,
            predicate,
            sorter,
            behavior,
            ct);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    public async Task<IList<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
        => await Search(
            selector,
            amount,
            offset,
            predicate,
            sorter,
            behavior)
            .ToListAsync(ct);

    public async Task<IList<TResult>> GetAllGroupedAsync<TResult, TGroupKey>(
        Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> groupSelector,
        Expression<Func<TEntity, TGroupKey>> groupBy,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default)
        => await SearchWithGrouping(
                groupSelector,
                groupBy,
                amount,
                offset,
                predicate,
                sorter,
                behavior)
            .ToListAsync(ct);

    #endregion Get/Find multiple entity

    #region Create entity(-ies)

    /// <summary>
    /// Create a entity, returns tracking entity
    /// </summary>
    /// <param name="TEntity"></param>
    public async ValueTask<TEntity> CreateAsync(
        TEntity noTrackingEntity,
        CancellationToken ct = default)
        => (await context.Set<TEntity>()
            .AddAsync(noTrackingEntity, ct))
            .Entity;

    /// <summary>
    /// Create a entities, returns tracking entities
    /// </summary>
    /// <param name="TEntity"></param>
    public async Task<IEnumerable<TEntity>> CreateAsync(
        IEnumerable<TEntity> noTrackingEntities,
        CancellationToken ct = default)
    {
        await context.Set<TEntity>()
            .AddRangeAsync(noTrackingEntities, ct);
        return noTrackingEntities;
    }

    public async Task CreateIfNotExistAsync(
        IEnumerable<TEntity> entities,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicateFactory,
        CancellationToken ct = default)
    {
        foreach (var entity in entities)
        {
            var predicate = predicateFactory(entity);

            var isExists = await context.Set<TEntity>()
                .AsNoTracking()
                .AnyAsync(predicate, ct);

            if (!isExists)
            {
                await context.Set<TEntity>().AddAsync(entity, ct);
            }
        }

        await context.SaveChangesAsync(ct);
    }

    #endregion Create entity(-ies)

    #region Update entity(-ies)

    /// <summary>
    /// Mark entity updated, returns tracking entity
    /// </summary>
    /// <param name="TEntity"></param>
    public ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        CancellationToken ct = default)
        => ValueTask.FromResult(
            context.Set<TEntity>()
                .Update(entity)
                .Entity);

    /// <summary>
    /// Mark entities updated
    /// </summary>
    /// <param name="TEntity"></param>
    public ValueTask UpdateAsync(
        IEnumerable<TEntity> entities,
        CancellationToken ct = default)
    {
        context.Set<TEntity>()
            .UpdateRange(entities);
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Mark entity property updated
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public ValueTask<TEntity> UpdateAsync<TProp>(
        TEntity entity,
        Expression<Func<TEntity, TProp?>> propertyExpression,
        CancellationToken ct = default)
    {
        if (propertyExpression.Body is not MemberExpression member)
        {
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a method, not a property");
        }

        if (member.Member is not PropertyInfo propInfo)
        {
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a field, not a property");
        }

        var type = typeof(TEntity);
        if (propInfo.ReflectedType != null
            && type != propInfo.ReflectedType
            && !type.IsSubclassOf(propInfo.ReflectedType))
        {
            throw new ArgumentException($"Expression '{propertyExpression}' refers to a property that is not from type {type}");
        }

        context.Entry(entity).Property(propInfo.Name).IsModified = true;
        return ValueTask.FromResult(entity);
    }

    #endregion Update entity(-ies)

    #region Delete entity(-ies)

    /// <summary>
    /// Mark entity deleted, returns entity if found otherwise null
    /// </summary>
    /// <param name="TEntity"></param>
    public async Task<TEntity?> TryDeleteByIdAsync(
        TId id,
        CancellationToken ct = default)
    {
        var entity = await FindByIdAsync(
            id: id,
            selector: x => new TEntity() { Id = id },
            ct: ct);
        return entity != null
            ? await DeleteAsync(entity, ct)
            : default;
    }

    /// <summary>
    /// Mark entity deleted, returns entity
    /// </summary>
    /// <param name="TEntity"></param>
    public async Task<TEntity> DeleteByIdAsync(
        TId id,
        CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(
            id: id,
            selector: x => new TEntity() { Id = id },
            ct: ct);
        return await DeleteAsync(entity, ct);
    }

    /// <summary>
    /// Mark entities updated, returns entities which were marked, not found entities will be not added to the final collection
    /// </summary>
    /// <param name="TEntity"></param>
    public async Task<IReadOnlyCollection<TEntity>> TryDeleteByIdAsync(
        IEnumerable<TId> ids,
        CancellationToken ct = default)
    {
        var entities = new List<TEntity>();
        foreach (var id in ids)
        {
            var entity = await FindByIdAsync(
                id: id,
                selector: x => new TEntity() { Id = id },
                ct: ct);

            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        await DeleteAsync(entities, ct);
        return entities;
    }

    /// <summary>
    /// Mark entities updated, returns entities
    /// </summary>
    /// <param name="TEntity"></param>
    public async Task<IReadOnlyCollection<TEntity>> DeleteByIdAsync(
        IEnumerable<TId> ids,
        CancellationToken ct = default)
    {
        var entities = new List<TEntity>(ids.Count());
        foreach (var id in ids)
        {
            var entity = await GetByIdAsync(
                id: id,
                selector: x => new TEntity() { Id = id },
                ct: ct);
            entities.Add(entity);
        }

        await DeleteAsync(entities, ct);
        return entities;
    }

    /// <summary>
    /// Mark entity updated, returns tracking entity
    /// </summary>
    /// <param name="TEntity"></param>
    public ValueTask<TEntity> DeleteAsync(
        TEntity entity,
        CancellationToken ct = default)
        => ValueTask.FromResult(
            context.Set<TEntity>()
                .Remove(entity)
                .Entity);

    /// <summary>
    /// Mark entities updated
    /// </summary>
    /// <param name="TEntity"></param>
    public ValueTask DeleteAsync(
        IEnumerable<TEntity> entities,
        CancellationToken ct = default)
    {
        context.Set<TEntity>()
            .RemoveRange(entities);
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Deletes entities matching the given predicate from the database.
    /// Saves changes immediately.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities to delete</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Number of entities deleted</returns>
    public ValueTask DeleteWhereAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
    {
        var affectedRows = context.Set<TEntity>()
            .Where(predicate)
            .ExecuteDeleteAsync(ct);

        return ValueTask.CompletedTask;
    }

    #endregion Delete entity(-ies)

    private IQueryable<TResult> Search<TResult>(
            Expression<Func<TEntity, TResult>> selector,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking)
        => context.Set<TEntity>()
            .AsTracking(behavior)
            .Filter(predicate)
            .Sort(sorter)
            .Paginate(amount, offset)
            .Select(selector);

    private IQueryable<TResult> SearchWithGrouping<TResult, TGroupKey>(
        Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> groupSelector,
        Expression<Func<TEntity, TGroupKey>> groupBy,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking)
        => context.Set<TEntity>()
            .AsTracking(behavior)
            .Filter(predicate)
            .Sort(sorter)
            .Paginate(amount, offset)
            .GroupBy(groupBy)
            .Select(groupSelector);
}
