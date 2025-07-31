using IGamingTest.Core.Mq.Messages;
using MediatR;

namespace IGamingTest.Infrastructure.Mq.Messages;

public interface IMediatrQuery<out TResponse>
    : IQuery<TResponse>,
    IRequest<object>;
