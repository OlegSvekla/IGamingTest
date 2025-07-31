using IGamingTest.Core.Entities.Meteorite;
using IGamingTest.Infrastructure.Ef.Setup;
using Microsoft.EntityFrameworkCore;

namespace IGamingTest.Infrastructure;

public interface IGameContext : IEfContext
{
    DbSet<MeteoriteEntity> Meteorites { get; }

    DbSet<GeoLocationEntity> Geos { get; }
}

public class GameContext(
    DbContextOptions options,
    IDbConnector connector,
    IDbSeeder seeder
    ) : EfContextBase(
        options,
        connector,
        seeder),
    IGameContext
{
    public DbSet<MeteoriteEntity> Meteorites { get; set; } = default!;

    public DbSet<GeoLocationEntity> Geos { get; set; } = default!;
}
