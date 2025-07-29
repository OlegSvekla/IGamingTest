namespace IGamingTest.Infrastructure.Hangfire.Configs;

public sealed class HangfireConfig
{
    public string ConnectionString { get; set; } = default!;
    public int WorkerCount { get; set; } = 20;
}
