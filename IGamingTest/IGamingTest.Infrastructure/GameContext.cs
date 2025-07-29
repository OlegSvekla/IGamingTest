using IGamingTest.Core.Entities.Meteorite;
using Microsoft.EntityFrameworkCore;

namespace IGamingTest.Infrastructure;

public class GameContext(
    DbContextOptions options
    ) : DbContext(options)
{
    public DbSet<MeteoriteEntity> Meteorites { get; set; }
}
