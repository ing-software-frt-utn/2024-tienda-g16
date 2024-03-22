using Domain.Aggregates.SesionAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class SesionRepository : Repository<Sesion>, ISesionRepository
{
    public SesionRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Sesion>> GetAllAsync()
    {
        return await DbContext.Set<Sesion>()
            .Include(s => s.Vendedor)
            .Include(s => s.PuntoDeVenta)
                .ThenInclude(pdv => pdv.Sucursal)
            .ToListAsync();
    }

    public async Task<Sesion?> GetByVendedorIdAsync(Guid vendedorId)
    {
        return await DbContext.Set<Sesion>()
            .Where(s => s.Vendedor.Id == vendedorId)
            .Include(s => s.Vendedor)
            .Include(s => s.PuntoDeVenta)
            .FirstOrDefaultAsync();
    }

    public async Task<Sesion?> GetByPuntoDeVentaIdAsync(Guid puntoDeVentaId)
    {
        return await DbContext.Set<Sesion>()
            .Where(s => s.PuntoDeVenta.Id == puntoDeVentaId)
            .Include(s => s.Vendedor)
            .Include(s => s.PuntoDeVenta)
            .FirstOrDefaultAsync();
    }

    public void RemoveAll()
    {
        DbContext.RemoveRange(DbContext.Set<Sesion>());
    }

    public async override Task<Sesion?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<Sesion>()
            .Where(s => s.Id == id)
            .Include(s => s.Vendedor)
            .Include(s => s.PuntoDeVenta)
                .ThenInclude(s => s.Sucursal)
            .FirstOrDefaultAsync();
    }
}
