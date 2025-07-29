namespace IGamingTest.Infrastructure.Ef.Repositories.Interfaces;

public interface IUow
    : IDisposable
{
    Task CommitAsync(
        CancellationToken ct = default);

    IMeteoriteRepository MeteoriteRepository { get; }
}

