using Domain.Aggregates.VendedorAggregate;

namespace Application.DTOs.Common;

public sealed record VendedorDTO
{
    public Guid Id;
    public string Legajo { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;

    public static VendedorDTO? ToDTO(Vendedor vendedor)
    {
        if (vendedor == null)
        {
            return null;
        }

        return new VendedorDTO
        {
            Id = vendedor.Id,
            Legajo = vendedor.Legajo,
            Nombre = vendedor.Nombre,
            Apellido = vendedor.Apellido,
        };
    }
}
