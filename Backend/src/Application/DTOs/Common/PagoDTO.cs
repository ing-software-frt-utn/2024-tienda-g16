using Domain.Aggregates.VentaAggregate.Entities;
using Domain.Aggregates.VentaAggregate.Enums;

namespace Application.DTOs.Common;

public sealed class PagoDTO
{
    public Guid Id { get; private set; }
    public DateTime? Fecha { get; private set; }
    public double Monto { get; private set; }
    public double Vuelto { get; private set; }
    public TipoDePago TipoDePago { get; private set; }
    public string Descripcion { get; private set; } = string.Empty;

    public static PagoDTO? ToDTO(Pago? pago)
    {
        if (pago == null) return null;

        return new PagoDTO()
        {
            Id = pago.Id,
            Fecha = pago.Fecha,
            Monto = pago.Monto,
            Vuelto = pago.Vuelto,
            TipoDePago = pago.TipoDePago,
            Descripcion = pago.Descripcion,
        };
    }
}
