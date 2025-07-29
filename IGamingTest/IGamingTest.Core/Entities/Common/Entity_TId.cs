using IGamingTest.Core.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace IGamingTest.Core.Entities.Common;

public abstract class Entity<TId>
    : IEntity<TId>
    where TId : IEquatable<TId>
{
    [Key]
    public virtual TId Id { get; set; } = default!;
}
