using Domain.Aggregates.VentaAggregate.Entities;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class CondicionTributariaRepository : Repository<CondicionTributaria>, ICondicionTributariaRepository
{
    public CondicionTributariaRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<CondicionTributaria>> GetAllAsync()
    {
        return await DbContext.Set<CondicionTributaria>()
            .ToListAsync();
    }
}
