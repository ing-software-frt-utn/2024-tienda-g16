namespace Presentation.Responses.BuscarArticulo;

public sealed record BuscarArticuloResponse(
    Guid Id,
    string CodigoArticulo,
    MarcaResponse Marca,
    CategoriaResponse Categoria,
    string Descripcion,
    double PrecioFinal,
    List<StockResponse> Stocks);

public sealed record MarcaResponse(
    string Nombre);

public sealed record CategoriaResponse(
    string Descripcion);

public sealed record StockResponse(
    Guid Id,
    int CantidadDisponible,
    int CantidadReservada,
    TalleResponse Talle,
    ColorResponse Color);

public sealed record TalleResponse(
    double Medida,
    TipoTalleResponse TipoTalle);

public sealed record ColorResponse(
    string Descripcion);

public sealed record TipoTalleResponse(
    string Descripcion);
