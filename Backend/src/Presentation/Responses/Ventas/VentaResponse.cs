using Domain.Aggregates.VentaAggregate.ValueObjects;
using Domain.Primitives;
using Presentation.Responses.GetSesionesActivas;

namespace Presentation.Responses.Ventas;

public sealed record VentaResponse(
    Guid Id,
    DateTime? CreadaUtc,
    bool Confirmada,
    DateTime? ConfirmadaUtc,
    double Total,
    VendedorResponse Vendedor,
    ClienteResponse Cliente,
    TipoDeComprobanteResponse TipoDeComprobante,
    PuntoDeVentaResponse PuntoDeVenta,
    List<LineaDeVentaResponse> LineasDeVenta,
    PagoResponse Pago,
    CodigoComprobanteResponse CodigoComprobante); 

public sealed record LineaDeVentaResponse(
    Guid Id,
    double PrecioUnitario,
    double Subtotal,
    int Cantidad,
    StockResponse Stock);

public sealed record StockResponse(
    Guid Id,
    int CantidadDisponible,
    int CantidadReservada,
    TalleResponse Talle,
    ColorResponse Color, 
    ArticuloResponse Articulo);

public sealed record TalleResponse(
    double Medida,
    TipoTalleResponse TipoTalle);

public sealed record TipoTalleResponse(
    string Descripcion);

public sealed record ColorResponse(
    string Descripcion);

public sealed record ArticuloResponse(
    Guid Id,
    string CodigoArticulo,
    MarcaResponse Marca,
    CategoriaResponse Categoria,
    string Descripcion);

public sealed record MarcaResponse(
    string Nombre);

public sealed record CategoriaResponse(
    string Descripcion);

public sealed record ClienteResponse(
    Guid Id,
    NumeroDocumentoResponse NumeroDocumento,
    string Nombre,
    string Apellido,
    CondicionTributariaResponse CondicionTributaria);

public sealed record TipoDeComprobanteResponse(
    Guid Id,
    string Descripcion);

public sealed record CondicionTributariaResponse(
    Guid Id,
    int Tipo,
    string Descripcion);

public sealed record VendedorResponse(
    Guid Id,
    string Legajo,
    string Nombre,
    string Apellido);

public sealed record PuntoDeVentaResponse(
    Guid Id,
    int Numero,
    SucursalResponse Sucursal);

public sealed record PagoResponse(
    DateTime Fecha, 
    double Monto,
    double Vuelto,
    TipoDePago TipoDePago,
    string Descripcion);

public sealed record CodigoComprobanteResponse(
    string Numero, 
    DateTime FechaVencimiento);

public sealed record NumeroDocumentoResponse(
    string Numero,
    int TipoDocumento,
    string Descripcion);