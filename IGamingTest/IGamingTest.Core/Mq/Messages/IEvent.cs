namespace IGamingTest.Core.Mq.Messages;

/// <summary>
/// Event is a transfer object for a bus, which should be represented as poco-object
/// and the result should never be sent back to the service which executed it.
/// <remark>
/// Important: event can contain multiple handlers in the same / other microservices
/// or no one
/// </remark>
/// </summary>
public interface IEvent : IMessage
{
}