using IGamingTest.Infrastructure.Hangfire.Authorization.Models;
using Microsoft.Extensions.Options;

namespace IGamingTest.Infrastructure.Hangfire.Providers;

public interface IHangfireUserCredentialsProvider
{
    HangfireUserCredentials GetUserCredentials();
}

public sealed class HangfireUserCredentialsProvider(
    IOptions<HangfireUserCredentials> options) : IHangfireUserCredentialsProvider
{
    private readonly HangfireUserCredentials userCredentials = options.Value;

    public HangfireUserCredentials GetUserCredentials()
        => userCredentials;
}
