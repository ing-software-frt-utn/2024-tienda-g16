using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.LineasDeVenta;

public class LineaDeVentaCantidadNegativaDomainException : DomainException
{
    public LineaDeVentaCantidadNegativaDomainException() : base(LineaDeVentaErrors.CantidadNegativa)
    {
    }
}
