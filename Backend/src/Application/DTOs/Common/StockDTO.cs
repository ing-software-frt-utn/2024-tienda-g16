using Domain.Aggregates.StockAggregate;

namespace Application.DTOs.Common;

public sealed class StockDTO
{
    public Guid Id { get; private set; }
    public int CantidadDisponible { get; private set; }
    public int CantidadReservada { get; private set; }
    public TalleDTO? Talle { get; private set; }
    public ColorDTO? Color { get; private set; }

    public static StockDTO? ToDTO(Stock? stock)
    {
        if (stock == null) return null;

        return new StockDTO
        {
            Id = stock.Id,
            CantidadDisponible = stock.CantidadDisponible,
            CantidadReservada = stock.CantidadReservada,
            Talle = TalleDTO.ToDTO(stock.Talle),
            Color = ColorDTO.ToDTO(stock.Color),
        };
    }
}
