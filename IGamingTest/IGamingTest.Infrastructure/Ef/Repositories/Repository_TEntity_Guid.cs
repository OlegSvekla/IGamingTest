using IGamingTest.Core.Interfaces.Entities;
using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;

namespace IGamingTest.Infrastructure.Ef.Repositories;

public class Repository<TEntity>(
    GameContext context
    ) : Repository<TEntity, int>(
        context),
        IRepository<TEntity>
    where TEntity : class, IEntity, new();
