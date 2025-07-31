namespace IGamingTest.Core.Mq.Messages;

/// <summary>
/// Create Command is a command which purpose is creating entity.
/// CreatedId should automatically be set by command handler to newly created entity.
/// </summary>
public interface ICreateCommand : ICommand
{
    /// <summary>
    /// It's important to update field in the handler
    /// </summary>
    Guid Id { get; set; }
}