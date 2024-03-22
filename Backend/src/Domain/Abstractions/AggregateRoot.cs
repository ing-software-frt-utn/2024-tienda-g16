using System.Security.Cryptography;

namespace Domain.Abstractions;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id) { }
}
