using IGamingTest.Core.Interfaces.Entities;
using IGamingTest.Infrastructure.Ef.Repositories.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IGamingTest.Infrastructure.Ef.Repositories.Interfaces;

public interface IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
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
    Task<TEntity?> FindByIdAsync(
        TId id,
        Expression<Func<TEntity, TEntity>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Find entity by id otherwise returns null
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TResult?> FindByIdAsync<TResult>(
        TId id,
        Expression<Func<TEntity, TResult>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(
        TId id,
        Expression<Func<TEntity, TEntity>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="id"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TResult> GetByIdAsync<TResult>(
        TId id,
        Expression<Func<TEntity, TResult>> selector,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default)
        where TResult : class;

    /// <summary>
    /// Find entity by predicate otherwise returns null
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TEntity?> FindAsync(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Find entity by predicate otherwise returns null
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TResult?> FindAsync<TResult>(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default);

    /// <summary>
    /// Get entity by id otherwise throw an InvalidOperationException
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="selector"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<TResult> GetAsync<TResult>(
        SearchModeEnum mode,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        string? errorMessageWhenNotFound = null,
        CancellationToken ct = default)
        where TResult : class;

    /// <summary>
    /// Whether entity exist by Id or not
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<bool> ExistByIdAsync(
        TId id,
        CancellationToken ct = default);

    /// <summary>
    /// Whether entity exist by Id or not
    /// </summary>
    /// <param name="TEntity"></param>
    /// <returns></returns>
    Task<bool> ExistByPropertyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);

    /// <summary>
    /// Count entities
    /// </summary>
    /// <param name="amount">Take limitted amount and count by predicate just limitted count</param>
    /// <param name="offset"></param>
    /// <param name="predicate"></param>
    /// <param name="sorter"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<int> CountAsync(
        int amount = int.MaxValue,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        CancellationToken ct = default);

    #endregion Get/Find one entity

    #region Get/Find multiple entity

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    Task<IList<TEntity>> GetAllAsync(
        Expression<Func<TEntity, TEntity>> selector,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);    //TODO: add includeDeleted

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns></returns>
    Task<IList<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);    //TODO: add includeDeleted

    Task<IList<TResult>> GetAllGroupedAsync<TResult, TGroupKey>(
        Expression<Func<IGrouping<TGroupKey, TEntity>, TResult>> groupSelector,
        Expression<Func<TEntity, TGroupKey>> groupBy,
        int? amount = null,
        int? offset = null,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? sorter = null,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.NoTracking,
        CancellationToken ct = default);

    #endregion Get/Find multiple entity

    #region Create entity(-ies)

    /// <summary>
    /// Create a entity, returns tracking entity
    /// </summary>
    /// <param name="TEntity"></param>
    ValueTask<TEntity> CreateAsync(
        TEntity noTrackingEntity,
        CancellationToken ct = default);

    /// <summary>
    /// Create a entities, returns tracking entities
    /// </summary>
    /// <param name="TEntity"></param>
    Task<IEnumerable<TEntity>> CreateAsync(
        IEnumerable<TEntity> noTrackingEntities,
        CancellationToken ct = default);

    Task CreateIfNotExistAsync(
        IEnumerable<TEntity> entities,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicateFactory,
        CancellationToken ct = default);

    #endregion Create entity(-ies)

    #region Update entity(-ies)

    /// <summary>
    /// Mark entity updated, returns tracking entity
    /// </summary>
    /// <param name="TEntity"></param>
    ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entities updated
    /// </summary>
    /// <param name="TEntity"></param>
    ValueTask UpdateAsync(
        IEnumerable<TEntity> entities,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entity property updated
    /// </summary>
    /// <typeparam name="TProp">
    /// Required to detect proper type and get rid of boxing for
    /// structs (in case if you want to change it to use object type)
    /// </typeparam>
    /// <param name="entity"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    ValueTask<TEntity> UpdateAsync<TProp>(
        TEntity entity,
        Expression<Func<TEntity, TProp?>> propertyExpression,
        CancellationToken ct = default);

    #endregion Update entity(-ies)

    #region Delete entity(-ies)

    /// <summary>
    /// Mark entity deleted, returns entity if found otherwise null
    /// </summary>
    /// <param name="TEntity"></param>
    Task<TEntity?> TryDeleteByIdAsync(
        TId id,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entity deleted, returns entity
    /// </summary>
    /// <param name="TEntity"></param>
    Task<TEntity> DeleteByIdAsync(
        TId id,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entities updated, returns entities which were marked, not found entities will be not added to the final collection
    /// </summary>
    /// <param name="TEntity"></param>
    Task<IReadOnlyCollection<TEntity>> TryDeleteByIdAsync(
        IEnumerable<TId> ids,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entities updated, returns entities
    /// </summary>
    /// <param name="TEntity"></param>
    Task<IReadOnlyCollection<TEntity>> DeleteByIdAsync(
        IEnumerable<TId> ids,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entity updated, returns tracking entity
    /// </summary>
    /// <param name="TEntity"></param>
    ValueTask<TEntity> DeleteAsync(
        TEntity entity,
        CancellationToken ct = default);

    /// <summary>
    /// Mark entities updated
    /// </summary>
    /// <param name="TEntity"></param>
    ValueTask DeleteAsync(
        IEnumerable<TEntity> entities,
        CancellationToken ct = default);

    ValueTask DeleteWhereAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);

    #endregion Delete entity(-ies)
}
