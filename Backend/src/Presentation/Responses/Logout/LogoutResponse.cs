namespace Application.AppServices.Authentication.Commands.Logout;

public sealed record LogoutResponse(
    VendedorResponse Vendedor,
    List<SesionResponse> Sesiones);

public sealed record SesionResponse(
    VendedorResponse Vendedor,
    PuntoDeVentaResponse PuntoDeVenta);

public sealed record VendedorResponse(
    Guid Id,
    string Nombre,
    string Apellido);

public sealed record PuntoDeVentaResponse(
    Guid PuntoDeVentaId,
    int Numero);