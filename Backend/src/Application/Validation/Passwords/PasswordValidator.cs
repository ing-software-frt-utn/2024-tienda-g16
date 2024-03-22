using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.Passwords;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
                .WithMessage("La clave no puede ser nula")
            .Must(p => Regex.IsMatch(p, PasswordRules.ValidPasswordRegex))
                .WithMessage("La clave debe tener numeros y letras")
            .MinimumLength(PasswordRules.MinLength)
                .WithMessage($"La clave debe tener al menos {PasswordRules.MinLength} caracteres");
    }
}
