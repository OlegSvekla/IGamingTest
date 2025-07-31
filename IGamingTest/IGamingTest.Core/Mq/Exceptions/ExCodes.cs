namespace IGamingTest.Core.Mq.Exceptions;

/// <summary>
/// Exception codes. Min: 60 000 Max: 69 999
/// </summary>
public enum ExCodes
{
    None = 60000,
    PoisonCommand,
    PoisonEvent,
    PoisonQuery,
}
