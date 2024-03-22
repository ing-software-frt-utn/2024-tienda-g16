using Domain.Abstractions;
using Domain.Common.Errors;

namespace Domain.Common.DomainExceptions.Stocks;

public class StockNoSePuedeConfirmarDomainException : DomainException
{
    public StockNoSePuedeConfirmarDomainException() : base(StockErrors.NoSePuedeConfirmar)
    {
    }
}
