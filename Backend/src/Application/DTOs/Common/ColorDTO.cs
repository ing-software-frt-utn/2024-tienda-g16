using Domain.Aggregates.StockAggregate.Entities;

namespace Application.DTOs.Common;

public sealed class ColorDTO
{
    public string Descripcion { get; private set; } = string.Empty;

    public static ColorDTO? ToDTO(Color? color)
    {
        if (color == null) return null;

        return new ColorDTO
        {
            Descripcion = color.Descripcion,
        };
    }
}
