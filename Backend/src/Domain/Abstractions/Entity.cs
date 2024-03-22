namespace Domain.Abstractions;

public abstract class Entity : IEquatable<Entity>, IHasDomainEvents
{
    private readonly List<IDomainEvent> _events = [];
    public Guid Id { get; private init; }
    protected Entity(Guid id)
    {
        Id = id;
    }
    public IReadOnlyList<IDomainEvent> DomainEvents { get => _events.ToList(); }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }
    public void ClearDomainEvents() => _events.Clear();
    public override bool Equals(object? obj)
    {
        if (this is null)
        {
            return false;
        }

        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        return Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity? first, Entity? second)
    {
        return Equals(first, second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
