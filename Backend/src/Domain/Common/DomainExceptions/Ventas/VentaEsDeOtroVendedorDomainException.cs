using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaEsDeOtroVendedorDomainException : DomainException
{
    public VentaEsDeOtroVendedorDomainException() : base(VentaErrors.EsDeOtroVendedor)
    {
    }
}
