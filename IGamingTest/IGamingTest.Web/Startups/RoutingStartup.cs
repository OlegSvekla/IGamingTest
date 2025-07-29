using IGamingTest.Core.Enums;

namespace IGamingTest.Web.Startups;

/// <summary>
/// Routing.
/// UseRouting doesn't need to be explicitly called if routes should be matched at the beginning of the middleware pipeline.
/// See <see cref="https://stackoverflow.com/questions/57846127/what-are-the-differences-between-app-userouting-and-app-useendpoints"/>
/// </summary>
public sealed class RoutingStartup : AppStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.Routing;

    public override ValueTask<WebApplication> UseAsync(
        WebApplication app)
    {
        app.UseRouting();

        return ValueTask.FromResult(app);
    }
}
