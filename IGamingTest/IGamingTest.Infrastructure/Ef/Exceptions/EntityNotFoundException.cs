using IGamingTest.Core.Attributes;
using IGamingTest.Core.Exceptions;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IGamingTest.Ef.Exceptions;

[HttpResponseStatusCode(Status404NotFound)]
public class EntityNotFoundException(
    string errorMessage,
    Exception? innerEx = null
    ) : DomainException(
        message: errorMessage,
        innerEx: innerEx)
{
    public override int Code => (int)ExCodes.EntityNotFound;

    public static void ThrowIfNull<T>(
        [NotNull] T? value,
        string nameOfField,
        string? errorMessage = null)
        where T : class
    {
        if (value == null)
        {
            var message = !string.IsNullOrWhiteSpace(errorMessage)
                ? errorMessage
                : $"Entity '{nameOfField}' has not been found";
            throw new EntityNotFoundException(message);
        }
    }
}
