using Domain.Abstractions;

namespace Domain.Aggregates.VentaAggregate.ValueObjects;

public sealed class CodigoComprobante : ValueObject
{
    private CodigoComprobante(
        string numero, 
        DateTime fechaVencimiento)
    {
        Numero = numero;
        FechaVencimiento = fechaVencimiento;
    }
    public string Numero { get; }
    public DateTime FechaVencimiento { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Numero;
        yield return FechaVencimiento;
    }

    public static CodigoComprobante Create(string numero, DateTime fechaVencimiento)
    {
        return new CodigoComprobante(numero, fechaVencimiento);
    }

#pragma warning disable CS8618
    private CodigoComprobante() { }
# pragma warning restore CS8618
}
