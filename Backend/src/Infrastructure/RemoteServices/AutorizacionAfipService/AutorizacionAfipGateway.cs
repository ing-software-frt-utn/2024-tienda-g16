using Application.Adapters;
using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.ValueObjects;
using Domain.RemoteServicesContracts;

namespace Infrastructure.RemoteServices.AutorizacionAfipService;

public class AutorizacionAfipGateway : IAutorizacionAfipGateway
{
    private readonly IAutorizacionAfipAdapter _adapter;

    public AutorizacionAfipGateway(IAutorizacionAfipAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<CodigoComprobante> AutorizarVenta(Venta venta)
    {
        var codigoComprobante = await _adapter.AutorizarVenta(venta);

        return codigoComprobante;
    }

    public double ObtenerMontoMaximoConsumidorFinal()
    {
        // Esto en realidad debería sacarlo de algún servicio externo
        return 191624.00;
    }
}
