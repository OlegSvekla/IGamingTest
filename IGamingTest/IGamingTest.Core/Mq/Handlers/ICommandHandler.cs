using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Handlers;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    ValueTask HandleAsync(TCommand command, CancellationToken ct);
}
