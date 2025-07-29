namespace IGamingTest.Core.Enums;

/// <summary>
/// Order middleware in right direction.
/// See <see cref="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware"/>
/// </summary>
public enum MiddlewareOrderEnum
{
    Asap = 1,
    Logger,
    ExceptionHandler,
    Hsts,
    StaticFiles,
    CookiePolicy,
    Routing,
    RateLimit,
    L11n,
    Cors,
    Validation,
    PreEndpoint,
    Jobs,
    Endpoint,
    Final = 1000
}
