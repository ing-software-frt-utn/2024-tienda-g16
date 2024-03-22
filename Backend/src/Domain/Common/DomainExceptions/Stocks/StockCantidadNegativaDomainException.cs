using Domain.Abstractions;
using Domain.Common.Errors;

namespace Domain.Common.DomainExceptions.Stocks;

public class StockCantidadNegativaDomainException : DomainException
{
    public StockCantidadNegativaDomainException() : base(StockErrors.CantidadNegativa)
    {
        
    }
}
