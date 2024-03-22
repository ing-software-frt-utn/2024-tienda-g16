using FluentValidation;

namespace Application.Validation.Ids;

public class IdValidator : AbstractValidator<string>
{
    public IdValidator()
    {
        RuleFor(x => x)
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage(IdErrors.IdInvalido.Description);
    }
}
