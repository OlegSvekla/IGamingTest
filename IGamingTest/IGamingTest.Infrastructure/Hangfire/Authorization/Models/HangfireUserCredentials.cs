namespace IGamingTest.Infrastructure.Hangfire.Authorization.Models;

public class HangfireUserCredentials
{
    public required string Username { get; init; }
    public required string Password { get; init; }

    public bool ValidateUser(string username, string password)
        => string.Equals(Username, username, StringComparison.OrdinalIgnoreCase) &&
           string.Equals(Password, password, StringComparison.Ordinal);
}
