using Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.NumerosDocumento;

public class CuilValidator : AbstractValidator<string>
{
    public CuilValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El cuil no puede ser nulo");

        RuleFor(x => x)
            .Must(n => Regex.IsMatch(n, NumeroDocumentoRules.OnlyNumbersAndNoPaddingRegex))
            .WithMessage("El cuil debe contener solo numeros y sin ceros a la izquierda");

        RuleFor(x => x)
            .Length(NumeroDocumentoRules.CuilLength)
            .WithMessage($"El cuil debe tener una longitud de {NumeroDocumentoRules.CuilLength} caracteres");

        RuleFor(x => x)
            .Must(n => IsValidCuil(n))
            .WithMessage("El cuil no cumple el algoritmo de verificacion");
    }
    public static bool IsValidCuil(string cuil)
    {
        if (cuil.Length != 11 || !Regex.IsMatch(cuil, @"^\d{11}$"))
        {
            return false;
        }

        int[] factores = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        int suma = 0;
        for (int i = 0; i < 10; i++)
        {
            suma += int.Parse(cuil[i].ToString()) * factores[i];
        }

        int resto = suma % 11;
        int digitoVerificador = resto == 0 ? 0 : (resto == 1 ? 9 : 11 - resto);

        return digitoVerificador == int.Parse(cuil[10].ToString());
    }
}
