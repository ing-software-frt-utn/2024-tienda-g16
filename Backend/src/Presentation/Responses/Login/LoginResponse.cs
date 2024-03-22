namespace Presentation.Responses.Login;
public sealed record LoginResponse(
    UserResponse User,
    SesionResponse Sesion,
    string Token);

public sealed record UserResponse(
    string Email);

public sealed record SesionResponse(
    VendedorResponse Vendedor,
    PuntoDeVentaResponse PuntoDeVenta);

public sealed record VendedorResponse(
    string Legajo,
    string Nombre,
    string Apellido);

public sealed record PuntoDeVentaResponse(
    string Id,
    int Numero,
    SucursalResponse Sucursal);

public sealed record SucursalResponse(
    string Id,
    string Nombre);
