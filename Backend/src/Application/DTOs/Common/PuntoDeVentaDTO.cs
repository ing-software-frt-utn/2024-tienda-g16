using Domain.Aggregates.PuntoDeVentaAggregate;

namespace Application.DTOs.Common;

public class PuntoDeVentaDTO
{
    public Guid Id { get; private set; }
    public SucursalDTO? Sucursal { get; private set; }
    public int Numero { get; private set; }

    public static PuntoDeVentaDTO? ToDTO(PuntoDeVenta? puntoDeVenta)
    {
        if (puntoDeVenta == null) return null;

        return new PuntoDeVentaDTO
        {
            Id = puntoDeVenta.Id,
            Sucursal = SucursalDTO.ToDTO(puntoDeVenta.Sucursal),
            Numero = puntoDeVenta.Numero,
        };
    }
}
