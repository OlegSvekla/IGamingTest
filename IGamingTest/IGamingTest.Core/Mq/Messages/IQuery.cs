namespace IGamingTest.Core.Mq.Messages;

/// <summary>
/// Query is a transfer object for a bus, which should be represented as poco-object
/// and the result should always be sent back to the service which executed it
/// </summary>
/// <remark>
/// Important: query can contain only 1 handler in the same microservice
/// where command has been created
/// </remark>
public interface IQuery<out TResult> : IQuery
{
}

public interface IQuery : IMessage
{
}
