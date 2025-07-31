using System.Diagnostics.CodeAnalysis;

namespace IGamingTest.Infrastructure.Ef.Helpers;

public static class NullableHelper
{
    public static T GetValue<T>(this T? value)
        where T : class
        => value ?? throw new NullReferenceException(nameof(T));

    public static T GetValue<T>(this T? value)
        where T : struct
        => value ?? throw new NullReferenceException(nameof(T));

    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T>? value)
        => value is null || !value.Any();

    public static bool IsNotNullAndNotEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? value)
        => value is not null && value.Any();
}
