namespace Application.Validation.NumerosDocumento;

public class NumeroDocumentoRules
{
    public const int CuitLength = 11;

    public const int CuilLength = 11;

    public const int DniMinLength = 7;
    public const int DniMaxLength = 8;

    public const string OnlyNumbersAndNoPaddingRegex = "^[1-9][0-9]*$";

}
