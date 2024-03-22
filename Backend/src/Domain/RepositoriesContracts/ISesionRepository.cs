using Domain.Aggregates.SesionAggregate;

namespace Domain.RepositoriesContracts;

public interface ISesionRepository
{
    Task<Sesion?> GetByVendedorIdAsync(Guid vendedorId);

    void Add(Sesion sesion);

    Task<Sesion?> GetByPuntoDeVentaIdAsync(Guid puntoDeVentaId);

    Task<List<Sesion>> GetAllAsync();

    Task<Sesion?> GetByIdAsync(Guid id);

    void Remove(Sesion sesion);

    void RemoveAll();
}
