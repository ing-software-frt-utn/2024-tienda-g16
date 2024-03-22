using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaYaConfirmadaDomainException : DomainException
{
    public VentaYaConfirmadaDomainException() : base(VentaErrors.VentaYaConfirmada)
    {
    }
}
