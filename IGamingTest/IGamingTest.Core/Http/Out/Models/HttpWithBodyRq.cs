namespace IGamingTest.Core.Http.Out.Models;

public record HttpWithBodyRq<TBody>(
    Uri Uri,
    HttpMethod Method,
    TBody Body,
    IDictionary<string, string>? Headers = null,
    IDictionary<string, string>? Queries = null
    ) : HttpRq(
        Uri,
        Method,
        Headers,
        Queries
        );
