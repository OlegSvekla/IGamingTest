namespace IGamingTest.Core.Exceptions;

/// <summary>
/// Base class for all sorts of custom exceptions
/// </summary>
public abstract class DomainException(
    string message,
    Exception? innerEx = null
    ) : ApplicationException(
        message: message,
        innerException: innerEx)
{
    public abstract int Code { get; }
}
