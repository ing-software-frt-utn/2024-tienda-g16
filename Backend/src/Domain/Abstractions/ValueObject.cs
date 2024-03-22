using System.Runtime.InteropServices.Marshalling;

namespace Domain.Abstractions;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEqual(other);
    }

    public static bool operator ==(ValueObject? first, ValueObject? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(ValueObject? first, ValueObject? second)
    {
        return !(first == second);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int), HashCode.Combine);
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }
}
