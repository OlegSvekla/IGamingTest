using IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Resources;

namespace IGamingTest.Infrastructure.Hangfire.CronExpressionGenerator.Utils;

internal static class CronGenerationValueValidator
{
    internal static void ValidateDays(List<int> days)
    {
        if (days.Count != days.Distinct().Count())
        {
            throw new ArgumentOutOfRangeException(nameof(days), days, ExceptionMessages.DuplicateDaysException);
        }
    }

    internal static void ValidateMonthsToRunAfter(int amountOfMonthsToRunAfter)
    {
        if (amountOfMonthsToRunAfter is < 1 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(amountOfMonthsToRunAfter), amountOfMonthsToRunAfter, ExceptionMessages.InvalidAmountOfMonthsToRunAfterException);
        }
    }

    internal static void ValidateMonthToRunOn(int month)
    {
        if (month is < 1 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month), month, ExceptionMessages.InvalidMonthParameterException);
        }
    }

    internal static void ValidateDayOfMonthForShorterMonth(int month, int dayToRunOn)
    {
        if (month is 4 or 6 or 9 or 11 && dayToRunOn > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(dayToRunOn), dayToRunOn, ExceptionMessages.InvalidShorterMonthParameterException);
        }
    }

    internal static void ValidateDayOfMonthForFebrary(int month, int dayToRunOn)
    {
        if (month == 2 && dayToRunOn > 29)
        {
            throw new ArgumentOutOfRangeException(nameof(dayToRunOn), dayToRunOn, ExceptionMessages.InvalidFebruaryDateParameterException);
        }
    }

    internal static void ValidateDayOfMonthToRunOn(int dayOfTheMonth)
    {
        if (dayOfTheMonth is < 1 or > 31)
        {
            throw new ArgumentOutOfRangeException(nameof(dayOfTheMonth), dayOfTheMonth, ExceptionMessages.InvalidDayOfTheMonthException);
        }
    }

    internal static void ValidateHours(int hours)
    {
        if (hours is < 1 or > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(hours), hours, ExceptionMessages.InvalidHourParameterException);
        }
    }

    internal static void ValidateMinutes(int minutes)
    {
        if (minutes is < 1 or > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(minutes), minutes, ExceptionMessages.InvalidMinuteParameterException);
        }
    }
}
