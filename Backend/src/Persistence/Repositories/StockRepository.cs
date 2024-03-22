using Domain.Aggregates.StockAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class StockRepository : Repository<Stock>, IStockRepository
{
    public StockRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Stock?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<Stock>()
            .Where(s => s.Id == id)
            .Include(s => s.Articulo)
            .Include(s => s.Sucursal)
            .FirstOrDefaultAsync();
    }
}
