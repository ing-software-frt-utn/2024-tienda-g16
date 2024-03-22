using Domain.Aggregates.VendedorAggregate;
using Domain.RepositoriesContracts;

namespace Persistence.Repositories;

internal sealed class VendedorRepository : Repository<Vendedor>, IVendedorRepository
{
    public VendedorRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }
}
