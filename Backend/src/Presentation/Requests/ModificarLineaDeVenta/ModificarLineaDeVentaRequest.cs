using Domain.Aggregates.PuntoDeVentaAggregate;

namespace Presentation.Requests.ModificarLineaDeVenta;

public sealed record ModificarLineaDeVentaRequest(
    string LineaDeVentaId, 
    int Cantidad);
