namespace IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Utils;

internal static class StringValidators
{
    internal static bool ValidateDayOfWeekStringValue(string value)
        => value switch
        {
            "SUN" => true,
            "MON" => true,
            "TUE" => true,
            "WED" => true,
            "THU" => true,
            "FRI" => true,
            "SAT" => true,
            _ => false,
        };

    internal static bool ValidateMonthStringValue(string value)
        => value switch
        {
            "JAN" => true,
            "FEB" => true,
            "MAR" => true,
            "APR" => true,
            "MAY" => true,
            "JUN" => true,
            "JUL" => true,
            "AUG" => true,
            "SEP" => true,
            "OCT" => true,
            "NOV" => true,
            "DEC" => true,
            _ => false,
        };
}
