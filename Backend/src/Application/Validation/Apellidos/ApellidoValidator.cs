using FluentValidation;

namespace Application.Validation.Apellidos;

public class ApellidoValidator : AbstractValidator<string>
{
    public ApellidoValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El apellido no puede ser nulo");

        RuleFor(x => x)
            .MaximumLength(ApellidoRules.MaxLength)
            .WithMessage($"El apellido tiene mas de {ApellidoRules.MaxLength} caracteres");
    }
}
