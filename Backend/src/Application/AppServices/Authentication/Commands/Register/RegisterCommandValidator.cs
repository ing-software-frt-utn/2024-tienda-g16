using Application.AppServices.Authentication.Commands.Login;
using Application.Validation.Emails;
using Application.Validation.Ids;
using Application.Validation.Passwords;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.AppServices.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(
        EmailValidator emailValidator,
        PasswordValidator passwordValidator, 
        IdValidator idValidator)
    {
        RuleFor(x => x.Email).SetValidator(emailValidator);
        RuleFor(x => x.Password).SetValidator(passwordValidator);
        RuleFor(x => x.VendedorId).SetValidator(idValidator);
    }
}
