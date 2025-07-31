using Microsoft.EntityFrameworkCore;

namespace IGamingTest.Infrastructure.Ef.Setup;

public interface IDbSeeder
{
    ValueTask SeedAsync(ModelBuilder modelBuilder);
}

public class DbSeeder : IDbSeeder
{
    public virtual ValueTask SeedAsync(ModelBuilder modelBuilder)
        => ValueTask.CompletedTask;
}
