using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.Entities;
using ErrorOr;

namespace Domain.GatewayContracts;

public interface IAutorizacionPagoTarjetaGateway
{
    Task AutorizarPagoTarjeta(Tarjeta tarjeta, Venta venta);
}
