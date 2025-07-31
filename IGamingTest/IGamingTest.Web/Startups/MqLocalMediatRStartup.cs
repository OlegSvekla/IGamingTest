using IGamingTest.Core.Enums;
using IGamingTest.Core.Startups;
using IGamingTest.Infrastructure.Mq;

namespace IGamingTest.Web.Startups;

/// <summary>
/// MediatR local message queue services.
/// </summary>
public sealed class MqLocalMediatRStartup : ServiceStartup
{
    public override ServiceRegistrationOrderEnum ServiceRegistrationOrder
        => ServiceRegistrationOrderEnum.Highest;

    public override ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddBatchMqLocalMediatR();

        return ValueTask.FromResult(services);
    }
}
