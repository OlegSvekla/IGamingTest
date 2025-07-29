namespace IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Utils;

public class CronTriggerDates
{
    public DateTime InitialTriggerTime { get; set; }

    public List<DateTime> TriggerDates { get; set; } = default!;
}
