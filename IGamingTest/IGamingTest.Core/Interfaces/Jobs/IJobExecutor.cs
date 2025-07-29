using System.Linq.Expressions;

namespace IGamingTest.Core.Interfaces.Jobs;

public interface IJobExecutor
{
    string Execute<T>(
        Expression<Action<T>> methodCall)
        where T : IJob;

    string Execute<T>(
        Expression<Func<T, Task>> methodCall)
        where T : IJob;

    string Schedule<T>(
        Expression<Action<T>> methodCall,
        TimeSpan delay)
        where T : IJob;

    string Schedule<T>(
        Expression<Func<T, Task>> methodCall,
        TimeSpan delay)
        where T : IJob;

    #region Recurring with Expression<Action<T>>

    /// <summary>
    /// Recurring job with custom cron expression, use CronGenerator or CronSpecialConsts.
    /// </summary>
    void Recurring<T>(
        Expression<Action<T>> methodCall,
        string cronExpression,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs every x minute(s)
    /// Example : A cron that will run every 2 minutes
    /// </summary>
    void RecurringEveryMinutes<T>(
        Expression<Action<T>> methodCall,
        int minutes,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs every x hour(s)
    /// Example : A cron that will run every 2 hours
    /// </summary>
    void RecurringEveryHours<T>(
        Expression<Action<T>> methodCall,
        int hours,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs at a given time every day of the week
    /// Example : A cron that will run at 13:00 every day
    /// </summary>
    void RecurringDaily<T>(
        Expression<Action<T>> methodCall,
        TimeSpan runTime,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs at a given time multiple times a week
    /// Example : A cron that will run at 13:00 every Monday, Wednesday, and Saturday
    /// </summary>
    void RecurringOnDays<T>(
        Expression<Action<T>> methodCall,
        TimeSpan runTime,
        List<DayOfWeek> daysToRun,
        string? jobId = null)
        where T : IJob;

    #endregion

    #region Recurring with Expression<Func<T, Task>>

    /// <summary>
    /// Recurring job with custom cron expression, use CronGenerator or CronSpecialConsts.
    /// </summary>
    void Recurring<T>(
        Expression<Func<T, Task>> methodCall,
        string cronExpression,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs every x minute(s)
    /// Example : A cron that will run every 2 minutes
    /// </summary>
    void RecurringEveryMinutes<T>(
        Expression<Func<T, Task>> methodCall,
        int minutes,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs every x hour(s)
    /// Example : A cron that will run every 2 hours
    /// </summary>
    void RecurringEveryHours<T>(
        Expression<Func<T, Task>> methodCall,
        int hours,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs at a given time every day of the week
    /// Example : A cron that will run at 13:00 every day
    /// </summary>
    void RecurringDaily<T>(
        Expression<Func<T, Task>> methodCall,
        TimeSpan runTime,
        string? jobId = null)
        where T : IJob;

    /// <summary>
    /// Generate a cron expression that runs at a given time multiple times a week
    /// Example : A cron that will run at 13:00 every Monday, Wednesday, and Saturday
    /// </summary>
    void RecurringOnDays<T>(
        Expression<Func<T, Task>> methodCall,
        TimeSpan runTime,
        List<DayOfWeek> daysToRun,
        string? jobId = null)
        where T : IJob;

    #endregion
}
