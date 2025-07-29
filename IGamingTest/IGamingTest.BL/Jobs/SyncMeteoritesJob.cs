using IGamingTest.BL.Services;
using IGamingTest.Core.Interfaces.Jobs;

namespace IGamingTest.BL.Jobs;

public class SyncMeteoritesJob(
    IMeteoriteService meteoriteDataClient
    ) : IJob
{
    public async Task ExecuteAsync(
        object? payload,
        CancellationToken ct)
        => await meteoriteDataClient.SyncMeteoritesFromExternalSourceAsync(ct);
}