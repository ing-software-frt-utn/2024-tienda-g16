using Domain.Aggregates.ArticuloAggregate.Entities;

namespace Application.DTOs.Common;

public sealed class CategoriaDTO
{
    public string Descripcion { get; private set; } = string.Empty;

    public static CategoriaDTO? ToDTO(Categoria? categoria)
    {
        if (categoria == null) return null;

        return new CategoriaDTO
        {
            Descripcion = categoria.Descripcion,
        };
    }
}
