using Application.DTOs.Common;

namespace Application.AppServices.Clientes.Commands.CrearCliente;

public sealed record CrearClienteResult(
    ClienteDTO? Cliente);
