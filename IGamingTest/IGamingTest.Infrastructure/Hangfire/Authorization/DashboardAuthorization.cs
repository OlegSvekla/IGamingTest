using Hangfire.Dashboard;
using IGamingTest.Infrastructure.Hangfire.Authorization.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;

namespace IGamingTest.Infrastructure.Hangfire.Authorization;

public class DashboardAuthorization(
    IEnumerable<HangfireUserCredentials> users
) : IDashboardAuthorizationFilter
{
    private IEnumerable<HangfireUserCredentials> Users { get; } = users;

    public bool Authorize(DashboardContext dashboardContext)
    {
        var context = dashboardContext.GetHttpContext();
        string header = context.Request.Headers.Authorization!;

        return string.IsNullOrWhiteSpace(header)
            ? Challenge(context)
            : !TryParseBasicAuth(header, out var username, out var password)
                ? Challenge(context)
                : ValidateCredentials(username, password) || Challenge(context);
    }

    private static bool TryParseBasicAuth(
        string header,
        out string username,
        out string password)
    {
        username = string.Empty;
        password = string.Empty;

        if (!AuthenticationHeaderValue.TryParse(header, out var authValues) || 
            !string.Equals(authValues.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) || 
            string.IsNullOrWhiteSpace(authValues.Parameter))
        {
            return false;
        }

        try
        {
            var parameter = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
            var parts = parameter.Split(':', 2); // Split into exactly two parts
            if (parts.Length != 2)
            {
                return false;
            }

            username = parts[0];
            password = parts[1];
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool ValidateCredentials(
        string username,
        string password)
        => !string.IsNullOrWhiteSpace(username)
            && !string.IsNullOrWhiteSpace(password)
            && Users.Any(user => user.ValidateUser(username, password));

    private static bool Challenge(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
        context.Response.WriteAsync("Authentication is required.");

        return false;
    }
}
