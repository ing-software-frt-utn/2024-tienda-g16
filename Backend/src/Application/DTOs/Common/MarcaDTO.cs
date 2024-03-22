using Domain.Aggregates.ArticuloAggregate.Entities;

namespace Application.DTOs.Common;

public sealed class MarcaDTO
{
    public string Nombre { get; private set; } = string.Empty;

    public static MarcaDTO? ToDTO(Marca? marca)
    {
        if (marca == null) return null;

        return new MarcaDTO
        {
            Nombre = marca.Nombre,
        };
    }
}
