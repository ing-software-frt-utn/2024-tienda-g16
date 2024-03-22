using Application.Abstractions.Messaging;

namespace Application.AppServices.Authentication.Commands.Login;

public record LoginCommand(
    string Email,
    string Password,
    string PuntoDeVentaId) : ICommand<LoginResult>;
