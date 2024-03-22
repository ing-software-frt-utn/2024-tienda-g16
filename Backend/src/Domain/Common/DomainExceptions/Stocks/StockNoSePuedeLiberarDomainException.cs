using Domain.Abstractions;
using Domain.Common.Errors;

namespace Domain.Common.DomainExceptions.Stocks;

public class StockNoSePuedeLiberarDomainException : DomainException
{
    public StockNoSePuedeLiberarDomainException() : base(StockErrors.NoSePuedeLiberar)
    {
    }
}
