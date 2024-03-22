using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Queries.ObtenerVentas;

public sealed record ObtenerVentasQuery(
    string SesionId) : IQuery<ObtenerVentasResult>;
