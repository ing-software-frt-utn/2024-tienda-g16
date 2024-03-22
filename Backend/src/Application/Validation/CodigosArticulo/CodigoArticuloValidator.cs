using FluentValidation;

namespace Application.Validation.CodigosArticulo;

public class CodigoArticuloValidator : AbstractValidator<string>
{
    public CodigoArticuloValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El codigo de articulo no puede ser nulo");

        RuleFor(x => x)
            .MaximumLength(CodigoArticuloRules.MaxLength)
            .WithMessage($"El codigo de articulo tiene mas de {CodigoArticuloRules.MaxLength} caracteres");

        RuleFor(x => x)
            .MinimumLength(CodigoArticuloRules.MinLength)
            .WithMessage($"El codigo de articulo tiene menos de {CodigoArticuloRules.MinLength} caracteres");
    }
}
