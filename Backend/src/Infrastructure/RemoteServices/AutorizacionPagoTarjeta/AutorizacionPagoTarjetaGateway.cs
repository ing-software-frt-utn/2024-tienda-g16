using Application.Adapters;
using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.Entities;
using Domain.GatewayContracts;
using ErrorOr;

namespace Infrastructure.RemoteServices.AutorizacionPagoTarjeta;

public class AutorizacionPagoTarjetaGateway : IAutorizacionPagoTarjetaGateway
{
    private readonly IAutorizacionPagoTarjetaAdapter _adapter;

    public AutorizacionPagoTarjetaGateway(IAutorizacionPagoTarjetaAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task AutorizarPagoTarjeta(
        Tarjeta tarjeta,
        Venta venta)
    {
        await _adapter.AutorizarPagoTarjeta(tarjeta, venta);
    }
}
