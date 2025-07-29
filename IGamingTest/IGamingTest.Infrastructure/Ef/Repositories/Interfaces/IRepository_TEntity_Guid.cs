using IGamingTest.Core.Interfaces.Entities;

namespace IGamingTest.Infrastructure.Ef.Repositories.Interfaces;

public interface IRepository<TEntity>
    : IRepository<TEntity, int>
    where TEntity : class, IEntity;

