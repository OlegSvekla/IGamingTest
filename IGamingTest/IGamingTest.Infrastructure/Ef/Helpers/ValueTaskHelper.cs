namespace IGamingTest.Infrastructure.Ef.Helpers;

public static class ValueTaskHelper
{
    /// <summary>
    /// Await result in sync code
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// Code was taken from
    /// https://stackoverflow.com/questions/72715689/valuetask-instances-should-not-have-their-result-directly-accessed-unless-the-in
    /// </remarks>
    public static void AwaitSync(this ValueTask value)
        => value.AsTask().GetAwaiter().GetResult();

    /// <summary>
    /// Await result in sync code
    /// </summary>
    /// <param name="value"></param>
    /// <remarks>
    /// Code was taken from
    /// https://stackoverflow.com/questions/72715689/valuetask-instances-should-not-have-their-result-directly-accessed-unless-the-in
    /// </remarks>
    public static T AwaitSync<T>(this ValueTask<T> value)
        => value.AsTask().GetAwaiter().GetResult();
}
