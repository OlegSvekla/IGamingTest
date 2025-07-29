using IGamingTest.Core.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IGamingTest.Core.Startups;

public interface ICustomStartup
{
    public ServiceRegistrationOrderEnum ServiceRegistrationOrder { get; }

    public MiddlewareOrderEnum MiddlewareOrder { get; }

    ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder);

    ValueTask<WebApplication> UseAsync(
        WebApplication app);
}

public abstract class BaseStartup : ICustomStartup
{
    public virtual ServiceRegistrationOrderEnum ServiceRegistrationOrder
        => ServiceRegistrationOrderEnum.Lowest;

    public abstract MiddlewareOrderEnum MiddlewareOrder { get; }

    public abstract ValueTask<IServiceCollection> AddAsync(
        IServiceCollection services,
        WebApplicationBuilder builder);

    public abstract ValueTask<WebApplication> UseAsync(
        WebApplication app);
}
