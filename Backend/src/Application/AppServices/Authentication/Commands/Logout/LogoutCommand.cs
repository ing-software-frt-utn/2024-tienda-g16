using Application.Abstractions.Messaging;

namespace Application.AppServices.Authentication.Commands.Logout;

public sealed record LogoutCommand (
    string SesionId) : ICommand<LogoutResult>;
