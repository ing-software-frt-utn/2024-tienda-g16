using Application.Abstractions.Messaging;

namespace Application.AppServices.Clientes.Queries.ObtenerCliente;

public sealed record ObtenerClientesQuery(
    string SesionId) : IQuery<ObtenerClientesResult>;
