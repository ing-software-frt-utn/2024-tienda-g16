using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaNoPuedeSerAnonimaDomainException : DomainException
{
    public VentaNoPuedeSerAnonimaDomainException() : base(VentaErrors.LaVentaNoPuedeSerAnonima)
    {
    }
}
