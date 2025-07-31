namespace IGamingTest.Core.Mq.Messages;

/// <summary>
/// Command is a transfer object for a bus, which should be represented as poco-object
/// and the result should never be sent back to the service which executed it
/// </summary>
/// <remark>
/// Important: command can contain only 1 handler in the same microservice
/// where command has been created
/// </remark>
public interface ICommand : IMessage
{
}