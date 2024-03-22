using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.ModificarLineaDeVenta;

public sealed record ModificarLineaDeVentaCommand(
    string SesionId,
    string VentaId, 
    string LineaDeVentaId, 
    int Cantidad) : ICommand<VentaResult>;
