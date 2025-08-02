namespace IGamingTest.Core.Http.Out.Models;

public record HttpRq(
    Uri Uri,
    HttpMethod Method,
    IDictionary<string, string>? Headers = null,
    IDictionary<string, string>? Queries = null
    );
