using IGamingTest.Infrastructure.Ef.Repositories.Interfaces;
using IGamingTest.Infrastructure.Repositories;

namespace IGamingTest.Infrastructure.Ef.Repositories;

public class Uow : IUow
{
    private bool disposed = false;
    private readonly GameContext context;

    public Uow(GameContext context)
    {
        this.context = context;

        MeteoriteRepository = new MeteoriteRepository(context);
    }

    public IMeteoriteRepository MeteoriteRepository { get; private set; }

    public async Task CommitAsync(
        CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }
}
