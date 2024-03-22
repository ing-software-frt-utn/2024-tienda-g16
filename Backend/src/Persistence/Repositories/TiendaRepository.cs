using Domain.Aggregates.TiendaAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class TiendaRepository : Repository<Tienda>, ITiendaRepository
{
    public TiendaRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public Tienda? GetTienda()
    {
        return DbContext.Set<Tienda>()
            .Include(t => t.CondicionTributaria)
            .Include(t => t.Sucursales)
                .ThenInclude(s => s.PuntosDeVenta)
            .FirstOrDefault();
    }
}
