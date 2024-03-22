using Domain.Common.ValueObjects;

namespace Application.DTOs.Common;

public class NumeroDocumentoDTO
{
    public string Numero { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public int TipoDocumento { get; private set; }
    public static NumeroDocumentoDTO? ToDTO(NumeroDocumento? numeroDocumento)
    {
        if (numeroDocumento is null)
        {
            return null;
        }

        return new NumeroDocumentoDTO()
        {
            Numero = numeroDocumento.Numero,
            Descripcion = numeroDocumento.Descripcion,
            TipoDocumento = (int)numeroDocumento.TipoDocumento,
        };
    }
}
