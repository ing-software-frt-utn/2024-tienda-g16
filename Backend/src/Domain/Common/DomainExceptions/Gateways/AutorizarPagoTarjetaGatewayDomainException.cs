using Domain.Abstractions;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Gateways;

public class AutorizarPagoTarjetaGatewayDomainException : DomainException
{
    public AutorizarPagoTarjetaGatewayDomainException(Error error) : base(error)
    {
    }
}
