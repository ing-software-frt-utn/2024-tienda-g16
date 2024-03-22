using Application.Abstractions.Messaging;

namespace Application.AppServices.Clientes.Commands.CrearCliente;

public sealed record CrearClienteCommand(
    string Nombre,
    string Apellido,
    string Numero,
    int TipoDocumento,
    string CondicionTributariaId,
    string SesionId) : ICommand<CrearClienteResult>;
