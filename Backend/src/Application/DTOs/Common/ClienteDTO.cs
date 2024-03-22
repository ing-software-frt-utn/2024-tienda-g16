using Domain.Aggregates.ClienteAggregate;

namespace Application.DTOs.Common;

public sealed record class ClienteDTO
{
    public Guid Id { get; private set; }
    public NumeroDocumentoDTO? NumeroDocumento { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public CondicionTributariaDTO? CondicionTributaria { get; private set; }

    public static ClienteDTO? ToDTO(Cliente? cliente)
    {
        if (cliente is null)
        {
            return null;
        }

        return new ClienteDTO
        {
            Id = cliente.Id,
            NumeroDocumento = NumeroDocumentoDTO.ToDTO(cliente.NumeroDocumento),
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            CondicionTributaria = CondicionTributariaDTO.ToDTO(cliente.CondicionTributaria),
        };

    }
}
