using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Queries.ObtenerVentaActual;

public sealed record ObtenerVentaActualQuery(
    string SesionId) : IQuery<VentaResult>;
