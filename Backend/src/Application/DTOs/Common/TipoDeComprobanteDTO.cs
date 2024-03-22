using Domain.Aggregates.VentaAggregate.Entities;

namespace Application.DTOs.Common;

public sealed record TipoDeComprobanteDTO
{
    public Guid Id { get; private set; }
    public string Descripcion { get; private set; } = string.Empty;

    public static TipoDeComprobanteDTO? ToDTO(TipoDeComprobante? tipoDeComprobante)
    {
        if (tipoDeComprobante == null) return null;

        return new TipoDeComprobanteDTO
        {
            Id = tipoDeComprobante.Id,
            Descripcion = tipoDeComprobante.Descripcion,
        };
    }
}
