using IGamingTest.Core.Mq.Messages;

namespace IGamingTest.Core.Mq.Handlers;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    ValueTask<TResponse> HandleAsync(TQuery query, CancellationToken ct);
}
