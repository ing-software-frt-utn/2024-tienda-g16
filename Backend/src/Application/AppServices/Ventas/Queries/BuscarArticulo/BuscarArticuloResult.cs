using Application.DTOs.Articulos;
using Application.DTOs.Common;

namespace Application.AppServices.Ventas.Queries.BuscarArticulo;

public sealed record BuscarArticuloResult(
    ArticuloDTO? Articulo,
    List<StockDTO?>? Stocks);
