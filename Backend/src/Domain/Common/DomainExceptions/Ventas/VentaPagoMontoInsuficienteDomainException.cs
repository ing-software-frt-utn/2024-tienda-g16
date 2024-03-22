using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaPagoMontoInsuficienteDomainException : DomainException
{
    public VentaPagoMontoInsuficienteDomainException() : base(VentaErrors.MontoInsuficiente)
    {
    }
}
