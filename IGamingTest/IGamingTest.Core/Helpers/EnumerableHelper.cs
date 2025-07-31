namespace IGamingTest.Core.Helpers;

public static class EnumerableHelper
{
    /// <summary>
    /// Gets index of value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks>
    /// Code was taken from
    /// https://stackoverflow.com/questions/1290603/how-to-get-the-index-of-an-element-in-an-ienumerable
    /// </remarks>
    public static int IndexOf<T>(this IEnumerable<T> source, T value)
    {
        var index = 0;
        var comparer = EqualityComparer<T>.Default;

        foreach (var item in source)
        {
            if (comparer.Equals(item, value))
            {
                return index;
            }

            index++;
        }

        return -1;
    }

    public static async Task<IReadOnlyCollection<T>> AsReadOnlyCollection<T>(this Task<T[]> value)
        => await value;

    public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> items)
        => (IReadOnlyCollection<T>)(object)items.ToArray();

    public static IReadOnlyCollection<T> ToOneItemReadOnlyCollection<T>(this T item)
        => item.ToOneItemList();

    public static List<T> ToOneItemList<T>(this T item)
        => [item];

    public static T[] ToOneItemArray<T>(this T item)
        => [item];

    public static bool IsOneItemCollection<T>(this ICollection<T> collection)
        => collection.Count == 1;

    public static IEnumerable<T> ForEach<T>(
        this IEnumerable<T> enumeration,
        Action<T> action)
    {
        foreach (var item in enumeration)
        {
            action(item);
        }

        return enumeration;
    }

    public static async Task ForEachAsync<T>(
        this IEnumerable<T> enumeration,
        Func<T, Task> action)
    {
        foreach (var item in enumeration)
        {
            await action(item);
        }
    }

    // Code was taken from https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
    public static IList<T> Shuffle<T>(this IList<T> value)
    {
        var random = new Random();

        var n = value.Count;
        while (n > 1)
        {
            var k = random.Next(n--);
            (value[k], value[n]) = (value[n], value[k]);
        }

        return value;
    }

    // Code was taken from https://gist.github.com/axelheer/b1cb9d7c267d6762b244
    public static double Median(this IEnumerable<int> source)
    {
        var data = source.OrderBy(n => n).ToArray();
        return !data.Any()
            ? throw new InvalidOperationException()
            : data.Length % 2 == 0
            ? (data[(data.Length / 2) - 1] + data[data.Length / 2]) / 2.0
            : data[data.Length / 2];
    }

    public static void AddIfNotNull<T>(this ICollection<T> source, T? value)
        where T : class
    {
        if (value is null)
        {
            return;
        }

        source.Add(value);
    }
}
