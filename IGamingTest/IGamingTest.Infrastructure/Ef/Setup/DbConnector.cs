using Microsoft.EntityFrameworkCore;

namespace IGamingTest.Infrastructure.Ef.Setup;

public interface IDbConnector
{
    ValueTask ConnectAsync(ModelBuilder modelBuilder);
}

public class DbConnector : IDbConnector
{
    public virtual ValueTask ConnectAsync(ModelBuilder modelBuilder)
        => ValueTask.CompletedTask;
}
