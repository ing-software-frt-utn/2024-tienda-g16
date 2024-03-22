using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaAsociarElMismoClienteDomainException : DomainException
{
    public VentaAsociarElMismoClienteDomainException() : base(VentaErrors.AsociarElMismoCliente)
    {
    }
}
