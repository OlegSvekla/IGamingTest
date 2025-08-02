namespace IGamingTest.Core.Cors.Configs;

public class CorsConfig
{
    /// <summary>
    /// A list of origins (URLs) that are allowed to make requests to the application.
    /// Example: "https://example.com", "https://api.example.com".
    /// </summary>
    public string[] AllowedOrigins { get; set; } = [];

    /// <summary>
    /// A list of HTTP methods that are allowed for cross-origin requests.
    /// Example: "GET", "POST", "PUT", "DELETE".
    /// </summary>
    public string[] AllowedMethods { get; set; } = [];
}
