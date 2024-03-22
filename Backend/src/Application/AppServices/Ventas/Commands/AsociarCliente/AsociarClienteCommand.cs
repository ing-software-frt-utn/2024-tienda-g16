using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.AsociarCliente;

public sealed record AsociarClienteCommand(
    string VentaId,
    string ClienteId,
    string SesionId) : ICommand<VentaResult>;