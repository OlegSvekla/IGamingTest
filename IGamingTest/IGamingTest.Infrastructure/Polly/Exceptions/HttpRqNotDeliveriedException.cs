using IGamingTest.Core.Exceptions;
using IGamingTest.Infrastructure.Polly.Exceptions;

namespace IGamingTest.Core.Polly.Exceptions;

public class HttpRqNotDeliveriedException(
    string message,
    Exception? innerEx = null
    ) : DomainException(
        message: message,
        innerEx: innerEx)
{
    public override int Code => (int)ExCodes.HttpRqNotDeliveried;

    public static HttpRqNotDeliveriedException CreateWhenUnknownEx(Exception innerEx, Uri uri, HttpMethod method)
        => new HttpRqNotDeliveriedException(
            message: $"Http request error: on uri '{uri}' on method '{method}' has not been success due to internal error",
            innerEx: innerEx);

    public static void ThrowWhenNotDileveredSuccessfully(bool delivered, Uri uri, HttpMethod method)
    {
        if (!delivered)
        {
            throw new HttpRqNotDeliveriedException(
                message: $"Http request error: on uri '{uri}' on method '{method}' has not been success");
        }
    }
}
