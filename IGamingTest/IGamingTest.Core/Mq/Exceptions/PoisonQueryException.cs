using IGamingTest.Core.Exceptions;
using IGamingTest.Core.Helpers;
using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Exceptions;

public class PoisonQueryException(
    IQuery query,
    Exception? innerEx = null
    ) : DomainException(
        message: $"Poison query '{query.GetType().GetFriendlyShortName()}' has been found",
        innerEx: innerEx)
{
    public override int Code => (int)ExCodes.PoisonQuery;
}
