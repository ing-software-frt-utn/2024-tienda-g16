using Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation.NumerosDocumento;

public class CuitValidator : AbstractValidator<string>
{
    public CuitValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("El cuit no puede ser nulo");

        RuleFor(x => x)
            .Must(n => Regex.IsMatch(n, NumeroDocumentoRules.OnlyNumbersAndNoPaddingRegex))
            .WithMessage("El cuit debe contener solo numeros y sin ceros a la izquierda");

        RuleFor(x => x)
            .Length(NumeroDocumentoRules.CuitLength)
            .WithMessage($"El cuit debe tener una longitud de {NumeroDocumentoRules.CuitLength} caracteres");

        RuleFor(x => x)
            .Must(n => IsValidCuit(n))
            .WithMessage("El cuit no cumple el algoritmo de verificacion");
    }

    public static bool IsValidCuit(string cuit)
    {
        if (cuit.Length != 11 || !Regex.IsMatch(cuit, @"^\d{11}$"))
        {
            return false;
        }

        int primerDigito = int.Parse(cuit[0].ToString());
        if (primerDigito != 2 && primerDigito != 3)
        {
            return false;
        }

        int[] factores = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        int suma = 0;
        for (int i = 0; i < 10; i++)
        {
            suma += int.Parse(cuit[i].ToString()) * factores[i];
        }

        int resto = suma % 11;
        int digitoVerificador = resto == 0 ? 0 : (resto == 1 ? 9 : 11 - resto);

        return digitoVerificador == int.Parse(cuit[10].ToString());
    }
}
