using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Buses;

public interface IQueryDispatcher
{
    ValueTask<TResponse> DispatchAsync<TResponse>(
        IQuery<TResponse> query, CancellationToken ct);
}
