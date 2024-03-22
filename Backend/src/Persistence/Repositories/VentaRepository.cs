using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.Aggregates.VendedorAggregate;
using Domain.Aggregates.VentaAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class VentaRepository : Repository<Venta>, IVentaRepository
{
    public VentaRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Venta?> GetByIdAsync(Guid id)
    {
        if (!await DbContext.Set<Venta>().AnyAsync(v => v.Id == id))
        {
            return null;
        }

        return await DbContext.Set<Venta>()
            .Where(v => v.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Venta?> GetByIdWithAllAsync(Guid id)
    {
        if (!await DbContext.Set<Venta>().AnyAsync(v => v.Id == id))
        {
            return null;
        }

        return await DbContext.Set<Venta>()
            .Where(v => v.Id == id)
            .Include(v => v.Cliente)
                .ThenInclude(v => v.CondicionTributaria)
            .Include(v => v.Pago)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Color)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Talle)
                        .ThenInclude(s => s.TipoTalle)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Articulo)
                        .ThenInclude(a => a.Categoria)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Articulo)
                        .ThenInclude(a => a.Marca)
            .Include(v => v.Vendedor)
            .Include(v => v.TipoDeComprobante)
            .Include(v => v.PuntoDeVenta)
            .Include(v => v.CodigoComprobante)
            .FirstOrDefaultAsync();
    }

    public async Task<Venta?> GetByVendedorAndPuntoDeVentaWithAllAsync(Vendedor vendedor, PuntoDeVenta puntoDeVenta)
    {
        if (!await DbContext.Set<Venta>().AnyAsync(v => v.Vendedor == vendedor && v.PuntoDeVenta == puntoDeVenta && !v.Confirmada))
        {
            return null;
        }

        return await DbContext.Set<Venta>()
            .Where(v => v.Vendedor == vendedor && v.PuntoDeVenta == puntoDeVenta && !v.Confirmada)
            .Include(v => v.Cliente)
                .ThenInclude(v => v.CondicionTributaria)
            .Include(v => v.Pago)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Color)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Talle)
                        .ThenInclude(s => s.TipoTalle)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Articulo)
                        .ThenInclude(a => a.Categoria)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Articulo)
                        .ThenInclude(a => a.Marca)
            .Include(v => v.Vendedor)
            .Include(v => v.TipoDeComprobante)
            .Include(v => v.PuntoDeVenta)
            .Include(v => v.CodigoComprobante)
            .FirstOrDefaultAsync();
    }

    public async Task<Venta?> GetByVendedorAndPuntoDeVentaAsync(Vendedor vendedor, PuntoDeVenta puntoDeVenta)
    {
        if (!await DbContext.Set<Venta>().AnyAsync(v => v.Vendedor == vendedor && v.PuntoDeVenta == puntoDeVenta && !v.Confirmada))
        {
            return null;
        }

        return await DbContext.Set<Venta>()
            .Where(v => v.Vendedor == vendedor && v.PuntoDeVenta == puntoDeVenta && !v.Confirmada)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Venta>> GetAllAsync()
    {
        return await DbContext.Set<Venta>()
            .Include(v => v.Cliente)
                .ThenInclude(v => v.CondicionTributaria)
            .Include(v => v.Pago)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Color)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Talle)
                        .ThenInclude(s => s.TipoTalle)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Articulo)
                        .ThenInclude(a => a.Categoria)
            .Include(v => v.LineasDeVenta)
                .ThenInclude(lv => lv.Stock)
                    .ThenInclude(s => s.Articulo)
                        .ThenInclude(a => a.Marca)
            .Include(v => v.Vendedor)
            .Include(v => v.TipoDeComprobante)
            .Include(v => v.PuntoDeVenta)
            .Include(v => v.CodigoComprobante)
            .ToListAsync();
    }
}
