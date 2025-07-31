using IGamingTest.Core.Exceptions;
using IGamingTest.Core.Helpers;
using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Exceptions;

public class PoisonCommandException(
    ICommand command,
    Exception? innerEx = null
    ) : DomainException(
        message: $"Poison command '{command.GetType().GetFriendlyShortName()}' has been found",
        innerEx: innerEx)
{
    public override int Code => (int)ExCodes.PoisonCommand;
}
