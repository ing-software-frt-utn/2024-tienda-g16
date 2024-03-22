using Domain.Aggregates.PuntoDeVentaAggregate;

namespace Domain.RepositoriesContracts;

public interface IPuntoDeVentaRepository
{
    Task<PuntoDeVenta?> GetByIdAsync(Guid id);
}
