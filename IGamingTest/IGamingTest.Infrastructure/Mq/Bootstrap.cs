using IGamingTest.Core.Mq.Buses;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.Infrastructure.Mq;

public static class Bootstrap
{
    public static void AddBatchMqLocalMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddScoped<ILocalMessageBus, MediatrLocalMessageBus>();
        services.AddScoped<IMediatrLocalMessageBus, MediatrLocalMessageBus>();
    }
}
