using IGamingTest.Core.Mq.Handlers;
using IGamingTest.Infrastructure.Mq.Messages;
using MediatR;

namespace IGamingTest.Infrastructure.Mq.Handlers;

public interface IMediatrEventHandler<TEvent>
    : IEventHandler<TEvent>,
    INotificationHandler<TEvent>
    where TEvent : IMediatrEvent;

public abstract class MediatrEventHandler<TEvent>
    : IMediatrEventHandler<TEvent>
    where TEvent : IMediatrEvent
{
    public Task Handle(
        TEvent notification,
        CancellationToken cancellationToken)
        => HandleAsync(notification, cancellationToken).AsTask();

    public abstract ValueTask HandleAsync(
        TEvent @event,
        CancellationToken ct);
}
