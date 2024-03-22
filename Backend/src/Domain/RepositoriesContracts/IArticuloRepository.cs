using Domain.Aggregates.ArticuloAggregate;

namespace Domain.RepositoriesContracts;

public interface IArticuloRepository
{
    Task<Articulo?> GetByCodigoArticuloAsync(string codigoArticulo);
}
