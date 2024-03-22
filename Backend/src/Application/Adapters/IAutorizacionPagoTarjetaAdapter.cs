using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.Entities;
using ErrorOr;

namespace Application.Adapters;

public interface IAutorizacionPagoTarjetaAdapter
{
    Task AutorizarPagoTarjeta(
        Tarjeta tarjeta, 
        Venta venta);
}
