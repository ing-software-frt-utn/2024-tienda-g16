using Domain.Abstractions;
using Domain.Aggregates.VentaAggregate.Enums;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Aggregates.VentaAggregate.Entities;

public class Pago : Entity
{
    private Pago(
        Guid id,
        DateTime fecha, 
        double monto, 
        double vuelto, 
        TipoDePago tipoDePago)
        : base(id)
    {
        TipoDePago = tipoDePago;
        Fecha = fecha;
        Monto = monto;
        Vuelto = vuelto;
        TipoDePago = tipoDePago;
    }

    public DateTime Fecha { get; private set; }
    public double Monto { get; private set; }
    public double Vuelto { get; private set; }
    public TipoDePago TipoDePago { get; private set; }
    public string Descripcion { get => TipoDePago.ToString(); }

    public void RealizarPagoConTarjeta(double monto)
    {
        Monto = monto;
        Vuelto = 0;
        Fecha = DateTime.UtcNow;
        TipoDePago = TipoDePago.PagoConTarjeta;
    }

    public static Pago PagoConTarjeta(double monto)
    {
        return new Pago(
            id: Guid.NewGuid(),
            fecha: DateTime.UtcNow,
            monto: monto, 
            vuelto: 0,
            tipoDePago: TipoDePago.PagoConTarjeta);
    }

    public static Pago PagoEnEfectivo(double montoAbonado, double montoAPagar)
    {
        return new Pago(
            id: Guid.NewGuid(),
            fecha: DateTime.UtcNow,
            monto: montoAPagar,
            vuelto: montoAbonado - montoAPagar,
            tipoDePago: TipoDePago.PagoEnEfectivo);
    }

# pragma warning disable CS8618
    private Pago(Guid id) : base(id) { }
# pragma warning restore CS8618
}
