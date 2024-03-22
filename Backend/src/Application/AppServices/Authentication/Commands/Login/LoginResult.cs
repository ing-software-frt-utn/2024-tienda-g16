using Application.DTOs.Common;

namespace Application.AppServices.Authentication.Commands.Login;
public sealed record LoginResult(
    UserDTO? User,
    SesionDTO? Sesion,
    string Token);
