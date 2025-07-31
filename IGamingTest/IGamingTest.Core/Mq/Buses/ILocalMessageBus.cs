namespace IGamingTest.Core.Mq.Buses;

public interface ILocalMessageBus
    : ICommandDispatcher,
    IEventDispatcher,
    IQueryDispatcher
{
}
