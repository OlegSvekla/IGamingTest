using IGamingTest.Infrastructure.Ef.Helpers;
using IGamingTest.Infrastructure.Ef.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IGamingTest.Infrastructure;

public interface IEfContext : IDisposable
{
    DatabaseFacade Database { get; }

    ChangeTracker ChangeTracker { get; }

    ValueTask InitAsync(CancellationToken ct);

    Task<int> SaveChangesAsync(CancellationToken ct = default);

    Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken ct = default);

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;
}

public abstract class EfContextBase(
    DbContextOptions options,
    IDbConnector connector,
    IDbSeeder seeder
    ) : DbContext(options),
        IEfContext
{
    public async virtual ValueTask InitAsync(
        CancellationToken ct)
    {
        if (!Database.IsRelational())
        {
            return;
        }

        await Database.MigrateAsync(ct);
    }

    public override int SaveChanges()
    {
        var result = base.SaveChanges();
        StopTracking();
        return result;
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken ct = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, ct);
        StopTracking();
        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        connector.ConnectAsync(builder).AwaitSync();
        seeder.SeedAsync(builder).AwaitSync();
    }

    private void StopTracking()
        => ChangeTracker.Clear();
}
