namespace IGamingTest.Core.Interfaces.Jobs;

public interface IJobSanitazer
{
    ValueTask DeleteAllExistingJobsAsync();

    ValueTask CleanupCompletedJobsAsync();
}
