using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Queries.BuscarArticulo;

public sealed record BuscarArticuloQuery(
    string SesionId,
    string CodigoArticulo) : IQuery<BuscarArticuloResult>;
