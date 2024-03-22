using FluentValidation;

namespace Application.Validation.AniosExpiracion;

public class AnioExpiracionValidator : AbstractValidator<int>
{
    public AnioExpiracionValidator()
    {
        RuleFor(x => x)
            .InclusiveBetween(AnioExpiracionRules.MinValue, AnioExpiracionRules.MaxValue)
            .WithMessage($"El anio de expiracion debe estar entre {AnioExpiracionRules.MinValue} y {AnioExpiracionRules.MaxValue}");
    }
}
