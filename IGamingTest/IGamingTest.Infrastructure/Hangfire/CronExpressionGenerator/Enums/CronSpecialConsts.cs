namespace IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Enums;

public static class CronSpecialConsts
{
    /// <summary>
    /// Run once, at startup.
    /// </summary>
    public const string Reboot = "@reboot";

    /// <summary>
    /// Run once a year, "0 0 1 1 *".
    /// </summary>
    public const string Yearly = "@yearly";

    /// <summary>
    /// (same as @yearly).
    /// </summary>
    public const string Annually = "@annually";

    /// <summary>
    /// Run once a month, "0 0 1 * *".
    /// </summary>
    public const string Monthly = "@monthly";

    /// <summary>
    /// Run once a week, "0 0 * * 0".
    /// </summary>
    public const string Weekly = "@weekly";

    /// <summary>
    /// Run once a day, "0 0 * * *".
    /// </summary>
    public const string Daily = "@daily";

    /// <summary>
    /// (same as @daily).
    /// </summary>
    public const string Midnight = "@midnight";

    /// <summary>
    /// Run once an hour, "0 * * * *".
    /// </summary>
    public const string Hourly = "@hourly";
}
