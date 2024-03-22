using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaNoSePuedePagarVaciaDomainException : DomainException
{
    public VentaNoSePuedePagarVaciaDomainException() : base(VentaErrors.NoSePuedePagarVentaVacia)
    {
    }
}
