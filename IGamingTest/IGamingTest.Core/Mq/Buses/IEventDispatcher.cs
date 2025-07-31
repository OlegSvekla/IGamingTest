using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Buses;

public interface IEventDispatcher
{
    ValueTask DispatchAsync(IEvent @event, CancellationToken ct);
}
