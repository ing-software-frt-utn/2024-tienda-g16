using Domain.Aggregates.SesionAggregate;

namespace Application.DTOs.Common;

public class SesionDTO
{
    public Guid Id { get; private set; }
    public VendedorDTO? Vendedor { get; private set; }
    public PuntoDeVentaDTO? PuntoDeVenta { get; private set; }

    public static SesionDTO? ToDTO(Sesion? sesion)
    {
        if (sesion == null) return null;

        return new SesionDTO
        {
            Id = sesion.Id,
            Vendedor = VendedorDTO.ToDTO(sesion.Vendedor),
            PuntoDeVenta = PuntoDeVentaDTO.ToDTO(sesion.PuntoDeVenta),
        };
    }
}
