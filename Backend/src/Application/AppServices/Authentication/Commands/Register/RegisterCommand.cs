using Application.Abstractions.Messaging;

namespace Application.AppServices.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string Email, 
    string Password, 
    string VendedorId) : ICommand<RegisterResult>;
