using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Handlers;

public interface IEventHandler<in TEvent>
    where TEvent : IEvent
{
    ValueTask HandleAsync(TEvent @event, CancellationToken ct);
}