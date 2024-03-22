using Domain.Abstractions;
using Domain.Common.Errors;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaStockDeOtraSucursalDomainException : DomainException
{
    public VentaStockDeOtraSucursalDomainException() : base(VentaErrors.StockDeOtraSucursal)
    {
    }
}
