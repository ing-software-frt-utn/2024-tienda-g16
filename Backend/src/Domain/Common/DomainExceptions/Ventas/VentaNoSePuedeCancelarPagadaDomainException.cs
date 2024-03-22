using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaNoSePuedeCancelarPagadaDomainException : DomainException
{
    public VentaNoSePuedeCancelarPagadaDomainException() : base(VentaErrors.NoSePuedeCancelarPagada)
    {
    }
}

