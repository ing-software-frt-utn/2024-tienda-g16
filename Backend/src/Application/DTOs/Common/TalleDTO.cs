using Domain.Aggregates.StockAggregate.Entities;

namespace Application.DTOs.Common;

public sealed class TalleDTO
{
    public double Medida { get; private set; }
    public TipoTalleDTO? TipoTalle { get; private set; }

    public static TalleDTO? ToDTO(Talle? talle)
    {
        if (talle == null) return null;

        return new TalleDTO
        {
            Medida = talle.Medida,
            TipoTalle = TipoTalleDTO.ToDTO(talle.TipoTalle),
        };
    }
}
