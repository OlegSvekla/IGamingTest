using IGamingTest.Core.Enums;

namespace IGamingTest.Web.Startups;

/// <summary>
/// Hsts.
/// The default HSTS value is 30 days. You may want to change this for production scenarios.
/// See <see cref="https://aka.ms/aspnetcore-hsts"/>
/// </summary>
public sealed class HstsStartup : AppStartup
{
    public override MiddlewareOrderEnum MiddlewareOrder
        => MiddlewareOrderEnum.Hsts;

    public override ValueTask<WebApplication> UseAsync(
        WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        return ValueTask.FromResult(app);
    }
}
