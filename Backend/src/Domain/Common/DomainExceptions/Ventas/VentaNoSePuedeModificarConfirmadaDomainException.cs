using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaNoSePuedeModificarConfirmadaDomainException : DomainException
{
    public VentaNoSePuedeModificarConfirmadaDomainException() : base(VentaErrors.NoSePuedeModificarConfirmada)
    {
    }
}
