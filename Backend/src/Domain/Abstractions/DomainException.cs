using ErrorOr;

namespace Domain.Abstractions;

public class DomainException : Exception
{
    public Error Error { get; }
    public DomainException(Error error)
    {
        Error = error;
    }
}
