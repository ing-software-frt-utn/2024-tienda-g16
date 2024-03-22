using Application.DTOs.Common;
using Domain.Aggregates.ArticuloAggregate;

namespace Application.DTOs.Ventas;

public sealed class ArticuloVentaDTO
{
    public Guid Id { get; private set; }
    public string CodigoArticulo { get; private set; } = string.Empty;
    public MarcaDTO? Marca { get; private set; }
    public CategoriaDTO? Categoria { get; private set; }
    public string Descripcion { get; private set; } = string.Empty;
    public double PrecioFinal { get; private set; }

    public static ArticuloVentaDTO? ToDTO(Articulo? articulo)
    {
        if (articulo == null) return null;

        return new ArticuloVentaDTO
        {
            Id = articulo.Id,
            CodigoArticulo = articulo.CodigoArticulo,
            Marca = MarcaDTO.ToDTO(articulo.Marca),
            Categoria = CategoriaDTO.ToDTO(articulo.Categoria),
            Descripcion = articulo.Descripcion,
            PrecioFinal = articulo.PrecioFinal,
        };
    }
}
