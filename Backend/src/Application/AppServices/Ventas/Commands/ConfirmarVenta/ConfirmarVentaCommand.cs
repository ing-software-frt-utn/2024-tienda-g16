using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.ConfirmarVenta;

public sealed record ConfirmarVentaCommand(
    string VentaId,
    string SesionId) : ICommand<VentaResult>;
