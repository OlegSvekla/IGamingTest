using IGamingTest.Core.Exceptions;
using IGamingTest.Core.Helpers;
using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Exceptions;

public class PoisonEventException(
    IEvent @event,
    Exception? innerEx = null
    ) : DomainException(
        message: $"Poison event '{@event.GetType().GetFriendlyShortName()}' has been found",
        innerEx: innerEx)
{
    public override int Code => (int)ExCodes.PoisonEvent;
}
