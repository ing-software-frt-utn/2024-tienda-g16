using Domain.Aggregates.SucursalAggregate;

namespace Application.DTOs.Common;

public class SucursalDTO
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;

    public static SucursalDTO? ToDTO(Sucursal? sucursal)
    {
        if (sucursal == null) return null;

        return new SucursalDTO
        {
            Id = sucursal.Id,
            Nombre = sucursal.Nombre,
        };
    }
}
