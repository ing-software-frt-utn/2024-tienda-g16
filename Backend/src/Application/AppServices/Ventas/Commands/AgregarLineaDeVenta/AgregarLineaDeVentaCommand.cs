using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.AgregarLineaDeVenta;

public sealed record AgregarLineaDeVentaCommand(
    string VentaId,
    string CodigoArticulo,
    string StockId,
    int Cantidad,
    string SesionId) : ICommand<VentaResult>;
