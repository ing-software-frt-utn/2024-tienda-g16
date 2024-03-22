using Application.DTOs.Ventas;
using Domain.Aggregates.VentaAggregate.Entities;

namespace Application.DTOs.Common;

public sealed record LineaDeVentaDTO
{
    public Guid Id { get; private set; }
    public double PrecioUnitario { get; private set; }
    public double Subtotal { get; private set; }
    public int Cantidad { get; private set; }
    public StockVentaDTO? Stock { get; private set; }

    public static LineaDeVentaDTO? ToDTO(LineaDeVenta? lineaDeVenta)
    {
        if (lineaDeVenta == null) return null;

        return new LineaDeVentaDTO
        {
            Id = lineaDeVenta.Id,
            PrecioUnitario = lineaDeVenta.PrecioUnitario,
            Subtotal = lineaDeVenta.GetSubtotal(),
            Cantidad = lineaDeVenta.Cantidad,
            Stock = StockVentaDTO.ToDTO(lineaDeVenta.Stock),
        };
    }
}
