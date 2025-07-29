using IGamingTest.BL.Jobs;
using IGamingTest.Core.Interfaces.Jobs;

namespace IGamingTest.Web;

public static class JobsRegistrator
{
    public static void StartupJobs(
        IServiceScope scope)
    {
        var jobExecutor = scope.ServiceProvider.GetRequiredService<IJobExecutor>();

        jobExecutor.RecurringEveryMinutes<SyncMeteoritesJob>(
            job => job.ExecuteAsync(null, CancellationToken.None),
            1,
            $"{nameof(SyncMeteoritesJob)}");
    }
}
