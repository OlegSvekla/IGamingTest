using IGamingTest.Core.Http;
using IGamingTest.Infrastructure.Ef.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IGamingTest.Core.Http.In;

public interface IHttpContextDataSearcher
{
    IReadOnlyCollection<string>? FindAuthTokens();

    string? FindIp();

    string? FindUserAgent();

    bool IsUserAuthorized();

    IReadOnlyCollection<Claim>? FindClaims();
}

public class HttpContextDataSearcher(
    IHttpContextAccessor contextAccessor
    ) : IHttpContextDataSearcher
{
    public IReadOnlyCollection<string>? FindAuthTokens()
        => contextAccessor.HttpContext?.Request.Headers.Authorization
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.GetValue())
            .ToArray();

    public string? FindIp()
        => contextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();

    public string? FindUserAgent()
        => contextAccessor.HttpContext?.Request.Headers[Consts.Header.Key.UserAgent].ToString();

    public bool IsUserAuthorized()
    {
        var claims = FindClaims();
        return claims != null && claims.Any();
    }

    public IReadOnlyCollection<Claim>? FindClaims()
        => contextAccessor.HttpContext?.User?.Claims?.ToList();
}
