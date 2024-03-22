using Domain.Abstractions;
using Domain.Common.Errors;

namespace Domain.Common.DomainExceptions.LineasDeVenta;

public class LineaDeVentaStockInsuficienteDomainException : DomainException
{
    public LineaDeVentaStockInsuficienteDomainException() : base(LineaDeVentaErrors.StockInsuficiente)
    {
    }
}
