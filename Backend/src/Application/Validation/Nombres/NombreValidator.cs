using FluentValidation;

namespace Application.Validation.Nombres;

public class NombreValidator : AbstractValidator<string>
{
    public NombreValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El nombre no puede ser nulo");

        RuleFor(x => x)
            .MaximumLength(NombreRules.MaxLength)
            .WithMessage($"El nombre tiene mas de {NombreRules.MaxLength} caracteres");
    }
}
