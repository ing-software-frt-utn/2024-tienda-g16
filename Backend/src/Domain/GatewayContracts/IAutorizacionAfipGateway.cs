using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.ValueObjects;
using ErrorOr;

namespace Domain.RemoteServicesContracts;

public interface IAutorizacionAfipGateway
{
    Task<CodigoComprobante> AutorizarVenta(Venta venta);

    double ObtenerMontoMaximoConsumidorFinal();
}
