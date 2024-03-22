using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Stocks;

public class StockNoSePuedeReservarDomainException : DomainException
{
    public StockNoSePuedeReservarDomainException() : base(StockErrors.NoSePuedeReservar)
    {
    }
}
