using Domain.Aggregates.StockAggregate.Entities;

namespace Application.DTOs.Common;

public sealed class TipoTalleDTO
{
    public string Descripcion { get; private set; } = string.Empty;

    public static TipoTalleDTO? ToDTO(TipoTalle? tipoTalle)
    {
        if (tipoTalle == null) return null;

        return new TipoTalleDTO
        {
            Descripcion = tipoTalle.Descripcion,
        };
    }
}
