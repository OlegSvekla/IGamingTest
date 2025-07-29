using IGamingTest.Core.Enums;
using Microsoft.AspNetCore.Builder;

namespace IGamingTest.Core.Startups;

public interface IServiceStartup : ICustomStartup
{
}

public abstract class ServiceStartup
    : BaseStartup,
    IServiceStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.Asap;

    public override ValueTask<WebApplication> UseAsync(
        WebApplication app)
        => ValueTask.FromResult(app);
}
