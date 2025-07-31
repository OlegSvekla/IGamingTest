using IGamingTest.Core.Mq.Messages;
using MediatR;

namespace IGamingTest.Infrastructure.Mq.Messages;

public interface IMediatrCommand : ICommand, IRequest;

public interface IMediatrCreateCommand : IMediatrCommand, ICreateCommand;
