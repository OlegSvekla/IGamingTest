using Hangfire;
using IGamingTest.Core.Interfaces.Jobs;
using IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator;
using System.Linq.Expressions;

namespace IGamingTest.Infrastructure.Hangfire.Execution;

public class HangfireJobExecutor : IJobExecutor
{
    public string Execute<T>(
        Expression<Action<T>> methodCall)
        where T : IJob
        => BackgroundJob.Enqueue(methodCall);

    public string Execute<T>(
        Expression<Func<T, Task>> methodCall)
        where T : IJob
        => BackgroundJob.Enqueue(methodCall);

    public string Schedule<T>(
        Expression<Action<T>> methodCall,
        TimeSpan delay)
        where T : IJob
        => BackgroundJob.Schedule(methodCall, delay);

    public string Schedule<T>(
        Expression<Func<T, Task>> methodCall,
        TimeSpan delay)
        where T : IJob
        => BackgroundJob.Schedule(methodCall, delay);

    public void Recurring<T>(
        Expression<Action<T>> methodCall,
        string cronExpression,
        string? jobId = null)
        where T : IJob
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            jobId = Guid.NewGuid().ToString();
        }

        RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
    }

    public void RecurringEveryMinutes<T>(
        Expression<Action<T>> methodCall,
        int minutes,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateMinutesCronExpression(minutes);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void RecurringEveryHours<T>(
        Expression<Action<T>> methodCall,
        int hours,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateHourlyCronExpression(hours);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void RecurringDaily<T>(
        Expression<Action<T>> methodCall,
        TimeSpan runTime,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateDailyCronExpression(runTime);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void RecurringOnDays<T>(
        Expression<Action<T>> methodCall,
        TimeSpan runTime,
        List<DayOfWeek> daysToRun,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateMultiDayCronExpression(runTime, daysToRun);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void Recurring<T>(
        Expression<Func<T, Task>> methodCall,
        string cronExpression,
        string? jobId = null)
        where T : IJob
    {
        if (string.IsNullOrWhiteSpace(jobId))
        {
            jobId = Guid.NewGuid().ToString();
        }

        RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
    }

    public void RecurringEveryMinutes<T>(
        Expression<Func<T, Task>> methodCall,
        int minutes,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateMinutesCronExpression(minutes);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void RecurringEveryHours<T>(
        Expression<Func<T, Task>> methodCall,
        int hours,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateHourlyCronExpression(hours);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void RecurringDaily<T>(
        Expression<Func<T, Task>> methodCall,
        TimeSpan runTime,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateDailyCronExpression(runTime);
        Recurring(methodCall, cronExpression, jobId);
    }

    public void RecurringOnDays<T>(
        Expression<Func<T, Task>> methodCall,
        TimeSpan runTime,
        List<DayOfWeek> daysToRun,
        string? jobId = null)
        where T : IJob
    {
        var cronExpression = CronGenerator.GenerateMultiDayCronExpression(runTime, daysToRun);
        Recurring(methodCall, cronExpression, jobId);
    }
}
