using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.CrearNuevaVenta;

public sealed record CrearNuevaVentaCommand(
    string SesionId) : ICommand<VentaResult>;
