namespace IGamingTest.Core.Http.Out.Helpers;

public static class HttpRequestMessageHelper
{
    public static HttpRequestMessage DeepCopy(
        this HttpRequestMessage rq)
    {
        var rqDeepCopy = new HttpRequestMessage
        {
            RequestUri = rq.RequestUri,
            Method = rq.Method,
            Content = rq.Content,
            Version = rq.Version,
            VersionPolicy = rq.VersionPolicy
        };
        rqDeepCopy.Headers.AddRange(rq.Headers);

        return rqDeepCopy;
    }
}
