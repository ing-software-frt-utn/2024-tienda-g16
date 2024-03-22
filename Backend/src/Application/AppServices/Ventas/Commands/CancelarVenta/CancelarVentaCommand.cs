using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.CancelarVenta;

public sealed record CancelarVentaCommand(
    string SesionId, 
    string VentaId) : ICommand;
