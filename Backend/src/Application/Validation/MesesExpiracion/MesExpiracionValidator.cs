using FluentValidation;

namespace Application.Validation.MesesExpiracion;

public class MesExpiracionValidator : AbstractValidator<int>
{
    public MesExpiracionValidator()
    {
        RuleFor(x => x)
            .InclusiveBetween(MesExpiracionRules.MinValue, MesExpiracionRules.MaxValue)
            .WithMessage($"El mes de expiracion debe estar entre {MesExpiracionRules.MinValue} y {MesExpiracionRules.MaxValue}");
    }
}
