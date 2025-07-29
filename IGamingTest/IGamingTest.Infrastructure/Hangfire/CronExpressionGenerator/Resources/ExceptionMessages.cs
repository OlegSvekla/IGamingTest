namespace IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Resources;

public static class ExceptionMessages
{
    public const string DuplicateDaysException = "Only one of each day of the week value can be passed in";
    public const string InvalidAmountOfMonthsToRunAfterException = "Month to run on parameter must be between 1 and 12";
    public const string InvalidDayOfTheMonthException = "Day of the month parameter must be between 1 and 31";
    public const string InvalidDayOfTheWeekParameterException = "Day of the week parameter should be between 0-6";
    public const string InvalidFebruaryDateParameterException = "February can only have a maximum of 29 days";
    public const string InvalidHourParameterException = "Hours parameter must be between 1 and 23";
    public const string InvalidMinuteParameterException = "Minutes parameter must be between 1 and 59";
    public const string InvalidMonthParameterException = "Month Parameter must be between 1 and 12";
    public const string InvalidShorterMonthParameterException = "The given month cannot have more than 30 days";
}
