using Domain.Aggregates.VentaAggregate.Entities;

namespace Domain.RepositoriesContracts;

public interface ITipoDeComprobanteRepository
{
    Task<TipoDeComprobante?> GetByCondicionesTributarias(CondicionTributaria seEmitePara, CondicionTributaria esEmitidoPor);
}
