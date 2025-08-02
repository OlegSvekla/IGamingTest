using IGamingTest.Core.Attributes;
using IGamingTest.Core.Exceptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IGamingTest.Core.Http.Exceptions;

[HttpResponseStatusCode(Status401Unauthorized)]
public class UnauthorizedRqException(
    Exception? innerEx = null
    ) : DomainException(ErrorMessage, innerEx)
{
    private const string ErrorMessage = "Access denied due to invalid credentials";

    public override int Code => (int)ExCodes.UnauthorizedRq;
}
