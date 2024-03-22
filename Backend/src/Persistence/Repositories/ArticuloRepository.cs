using Domain.Aggregates.ArticuloAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ArticuloRepository : Repository<Articulo>, IArticuloRepository
{
    public ArticuloRepository(TiendaDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Articulo?> GetByCodigoArticuloAsync(string codigoArticulo)
    {
        return await DbContext.Set<Articulo>()
            .Where(a => a.CodigoArticulo == codigoArticulo)
            .Include(a => a.Categoria)
            .Include(a => a.Marca)
            .Include(a => a.Stocks)
                .ThenInclude(s => s.Talle)
                .ThenInclude(s => s.TipoTalle)
            .Include(a => a.Stocks)
                .ThenInclude(s => s.Color)
            .FirstOrDefaultAsync();
    }
}
