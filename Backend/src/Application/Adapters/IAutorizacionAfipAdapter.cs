using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.ValueObjects;

namespace Application.Adapters;

public interface IAutorizacionAfipAdapter
{
    Task<CodigoComprobante> AutorizarVenta(Venta venta);
}
