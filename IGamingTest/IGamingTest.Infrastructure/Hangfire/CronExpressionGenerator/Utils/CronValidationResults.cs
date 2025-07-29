namespace IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Utils;

public record CronValidationResults(
    bool IsValidCron,
    string ValidationMessage
    );
