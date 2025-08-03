using Hangfire;
using Hangfire.Storage;
using IGamingTest.Core.Interfaces.Jobs;

namespace IGamingTest.Infrastructure.Hangfire.Sanitization;

public sealed class HangfireJobSanitazer : IJobSanitazer
{
    public ValueTask DeleteAllExistingJobsAsync()
    {
        var monitor = JobStorage.Current.GetMonitoringApi();

        var processingJobs = monitor.ProcessingJobs(0, int.MaxValue).ToList();
        processingJobs.ForEach(x => BackgroundJob.Delete(x.Key));

        var scheduledJobs = monitor.ScheduledJobs(0, int.MaxValue).ToList();
        scheduledJobs.ForEach(x => BackgroundJob.Delete(x.Key));

        using var connection = JobStorage.Current.GetConnection();
        var recurringJobs = connection.GetRecurringJobs();
        //recurringJobs.ForEach(x => RecurringJob.RemoveIfExists(x.Id));

        return ValueTask.CompletedTask;
    }

    public ValueTask CleanupCompletedJobsAsync()
    {
        var monitor = JobStorage.Current.GetMonitoringApi();

        var succeededJobs = monitor.SucceededJobs(0, int.MaxValue);
        succeededJobs.ForEach(x => BackgroundJob.Delete(x.Key));

        return ValueTask.CompletedTask;
    }
}
