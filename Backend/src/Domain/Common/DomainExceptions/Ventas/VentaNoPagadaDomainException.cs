using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaNoPagadaDomainException : DomainException
{
    public VentaNoPagadaDomainException() : base(VentaErrors.VentaNoPagada)
    {
    }
}
