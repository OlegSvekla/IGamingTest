namespace IGamingTest.Core.Interfaces.Jobs;

public interface IJob
{
    Task ExecuteAsync(
        object? payload,
        CancellationToken ct);
}
