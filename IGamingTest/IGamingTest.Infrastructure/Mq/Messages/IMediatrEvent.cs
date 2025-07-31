using IGamingTest.Core.Mq.Messages;
using MediatR;

namespace IGamingTest.Infrastructure.Mq.Messages;

public interface IMediatrEvent : IEvent, INotification;
