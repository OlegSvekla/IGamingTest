namespace IGamingTest.Core.Mq.Buses;

public interface IDistributedMessageBus
    : ICommandDispatcher,
    IEventDispatcher,
    IQueryDispatcher
{
}
