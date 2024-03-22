using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class PuntoDeVentaRepository : Repository<PuntoDeVenta>, IPuntoDeVentaRepository
{
    public PuntoDeVentaRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public async override Task<PuntoDeVenta?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<PuntoDeVenta>()
            .Where(pdv => pdv.Id == id)
            .Include(pdv => pdv.Sucursal)
            .FirstOrDefaultAsync();
    }
}
