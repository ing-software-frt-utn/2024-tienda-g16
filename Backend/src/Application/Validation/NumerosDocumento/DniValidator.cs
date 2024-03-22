using Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.NumerosDocumento;

public class DniValidator : AbstractValidator<string>
{
    public DniValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El dni no puede ser nulo");

        RuleFor(x => x)
            .Must(n => Regex.IsMatch(n, NumeroDocumentoRules.OnlyNumbersAndNoPaddingRegex))
            .WithMessage("El dni debe contener solo numeros y sin ceros a la izquierda");

        RuleFor(x => x.Length)
            .InclusiveBetween(NumeroDocumentoRules.DniMinLength, NumeroDocumentoRules.DniMaxLength)
            .WithMessage($"El dni debe tener entre {NumeroDocumentoRules.DniMinLength} y {NumeroDocumentoRules.DniMaxLength} caracteres");
    }
}
