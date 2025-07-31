using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Buses;

public interface ICommandDispatcher
{
    ValueTask DispatchAsync(ICommand command, CancellationToken ct);
}
