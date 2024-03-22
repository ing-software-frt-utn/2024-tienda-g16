using Application.Validation.Emails;
using Application.Validation.Ids;
using Application.Validation.Passwords;
using FluentValidation;

namespace Application.AppServices.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(
        IdValidator idValidator)
    {
        RuleFor(x => x.PuntoDeVentaId).SetValidator(idValidator);
    }
}
