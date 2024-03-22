using Domain.Aggregates.VentaAggregate.ValueObjects;

namespace Application.DTOs.Ventas;

public class CodigoComprobanteDTO
{
    public string Numero { get; private set; } = string.Empty;
    public DateTime FechaVencimiento { get; private set; }

    public static CodigoComprobanteDTO? ToDTO(CodigoComprobante? codigoComprobante)
    {
        if (codigoComprobante is null)
        {
            return null;
        }

        return new CodigoComprobanteDTO()
        {
            Numero = codigoComprobante.Numero,
            FechaVencimiento = codigoComprobante.FechaVencimiento,
        };
    }
}
