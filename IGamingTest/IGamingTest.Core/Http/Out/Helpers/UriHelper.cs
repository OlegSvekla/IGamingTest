namespace IGamingTest.Core.Http.Out.Helpers;

public static class UriHelper
{
    private const char QueryStartSymbol = '?';
    private const char QueryKeyValueJoiner = '=';
    private const char QuerySeparator = '&';

    public static Uri BuildUriWithQueries(
        this Uri uri,
        IDictionary<string, string>? queries)
    {
        if (queries == null)
        {
            return uri;
        }

        var paths = queries
            .Select(query => $"{query.Key}{QueryKeyValueJoiner}{query.Value}")
            .ToList();
        var pathStr = string.Join(QuerySeparator, paths);
        var uriStr = $"{uri}{QueryStartSymbol}{pathStr}";
        var result = new Uri(uriStr);
        return result;
    }
}
