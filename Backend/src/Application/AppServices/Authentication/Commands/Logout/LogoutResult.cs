using Application.DTOs.Common;

namespace Application.AppServices.Authentication.Commands.Logout;

public sealed record LogoutResult(
    VendedorDTO? Vendedor,
    List<SesionDTO?>? Sesiones);