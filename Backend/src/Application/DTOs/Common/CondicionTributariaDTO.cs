using Domain.Aggregates.VentaAggregate.Entities;

namespace Application.DTOs.Common;

public sealed record CondicionTributariaDTO
{
    public Guid Id { get; private set; }
    public int Tipo { get; private set; }
    public string Descripcion { get; private set; } = string.Empty;
    public static CondicionTributariaDTO? ToDTO(CondicionTributaria? condicionTributaria)
    {
        if (condicionTributaria == null)
        {
            return null;
        }

        return new CondicionTributariaDTO
        {
            Id = condicionTributaria.Id,
            Tipo = (int)condicionTributaria.Tipo,
            Descripcion = condicionTributaria.Descripcion
        };
    }
}
