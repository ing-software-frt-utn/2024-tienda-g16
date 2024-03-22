using Domain.Aggregates.VentaAggregate.Entities;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal class TipoDeComprobanteRepository : Repository<TipoDeComprobante>, ITipoDeComprobanteRepository
{
    public TipoDeComprobanteRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TipoDeComprobante?> GetByCondicionesTributarias(CondicionTributaria seEmitePara, CondicionTributaria esEmitidoPor)
    {
        return await DbContext.Set<TipoDeComprobante>()
            .Where(tc => 
                tc.SeEmitePara.Any(sp => sp.CondicionTributaria == seEmitePara) && 
                tc.EsEmitidoPor.Any(ep => ep.CondicionTributaria == esEmitidoPor))
            .FirstOrDefaultAsync();
    }
}
