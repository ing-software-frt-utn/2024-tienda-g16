using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.Emails;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El email no puede ser nulo")
            .Must(e => Regex.IsMatch(e, EmailRules.ValidMailRegex))
            .WithMessage("El email no es valido");
    }
}
