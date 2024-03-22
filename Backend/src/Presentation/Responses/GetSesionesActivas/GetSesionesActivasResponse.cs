namespace Presentation.Responses.GetSesionesActivas;

public sealed record GetSesionesActivasResponse(
    List<SesionResponse> Sesiones);

public sealed record SesionResponse(
    Guid Id,
    VendedorResponse Vendedor,
    PuntoDeVentaResponse PuntoDeVenta);

public sealed record VendedorResponse(
    string Legajo,
    string Nombre, 
    string Apellido);

public sealed record PuntoDeVentaResponse(
    Guid Id,
    int Numero,
    SucursalResponse Sucursal);

public sealed record SucursalResponse(
    Guid Id, 
    string Nombre);