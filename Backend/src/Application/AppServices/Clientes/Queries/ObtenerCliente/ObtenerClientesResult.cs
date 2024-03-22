using Application.DTOs.Common;

namespace Application.AppServices.Clientes.Queries.ObtenerCliente;

public sealed record ObtenerClientesResult(
    List<ClienteDTO?> Clientes);
