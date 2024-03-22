using Domain.Abstractions;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Gateways;

public class AutorizacionAfipGatewayDomainException : DomainException
{
    public AutorizacionAfipGatewayDomainException(Error error) : base(error)
    {
    }
}
