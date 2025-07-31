using IGamingTest.Core.Mq.Handlers;
using IGamingTest.Infrastructure.Mq.Messages;
using MediatR;

namespace IGamingTest.Infrastructure.Mq.Handlers;

public interface IMediatrCommandHandler<TCommand>
    : ICommandHandler<TCommand>,
    IRequestHandler<TCommand>
    where TCommand : IMediatrCommand;

public abstract class MediatrCommandHandler<TCommand>
    : IMediatrCommandHandler<TCommand>
    where TCommand : IMediatrCommand
{
    public Task Handle(
        TCommand request,
        CancellationToken cancellationToken)
        => HandleAsync(request, cancellationToken).AsTask();

    public abstract ValueTask HandleAsync(
        TCommand command,
        CancellationToken ct);
}
