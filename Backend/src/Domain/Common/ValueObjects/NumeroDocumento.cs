using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Common.ValueObjects;

public sealed class NumeroDocumento : ValueObject
{
    private NumeroDocumento(
        TipoDocumento tipoDocumento,
        string numero)
    {
        TipoDocumento = tipoDocumento;
        Numero = numero;
    }
    public string Numero { get; }
    public TipoDocumento TipoDocumento { get; }
    public string Descripcion { get => TipoDocumento.ToString(); private set => TipoDocumento.ToString(); }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return TipoDocumento;
        yield return Numero;
    }
    public static NumeroDocumento Create(int tipoDocumentoNum, string numero)
    {
        if(!Enum.IsDefined(typeof(TipoDocumento), tipoDocumentoNum))
        {
            tipoDocumentoNum = 0;
        }

        return new NumeroDocumento(
            (TipoDocumento)tipoDocumentoNum,
            numero);
    }

#pragma warning disable CS8618
    private NumeroDocumento(
        TipoDocumento tipoDocumento,
        string numero,
        string descripcion)
    {
        TipoDocumento = tipoDocumento;
        Numero = numero;
        Descripcion = descripcion;
    }
# pragma warning restore CS8618
}
