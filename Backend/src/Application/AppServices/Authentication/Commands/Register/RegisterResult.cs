using Application.DTOs.Common;

namespace Application.AppServices.Authentication.Commands.Register;

public sealed record RegisterResult(
    UserDTO? User,
    VendedorDTO? Vendedor);
