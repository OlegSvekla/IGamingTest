namespace IGamingTest.Core.Interfaces.Entities;

public interface IEntity<TId>
    where TId : IEquatable<TId>
{
    TId Id { get; set; }
}
