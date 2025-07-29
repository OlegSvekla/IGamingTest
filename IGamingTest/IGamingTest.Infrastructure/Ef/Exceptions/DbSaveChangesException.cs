using IGamingTest.Core.Exceptions;

namespace IGamingTest.Ef.Exceptions;

public class DbSaveChangesException(
    Exception? innerEx = null
    ) : DomainException(ErrorMessage, innerEx)
{
    private const string ErrorMessage = "Exception during save db changes";
    public override int Code => (int)ExCodes.DbSaveChanges;
}
