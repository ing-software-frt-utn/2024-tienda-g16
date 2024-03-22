using Domain.Aggregates.VendedorAggregate;

namespace Domain.RepositoriesContracts;

public interface IVendedorRepository
{
    Task<Vendedor?> GetByIdAsync(Guid id);
}
