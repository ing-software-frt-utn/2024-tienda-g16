using Domain.Aggregates.ClienteAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cliente?> GetByNumeroDocumentoAsync(string numeroDocumento)
    {
        return await DbContext.Set<Cliente>()
            .Where(c => c.NumeroDocumento.Numero == numeroDocumento)
            .Include(c => c.CondicionTributaria)
            .FirstOrDefaultAsync();
    }

    public override async Task<Cliente?> GetByIdAsync(Guid clienteId)
    {
        return await DbContext.Set<Cliente>()
            .Where(c => c.Id == clienteId)
            .Include(c => c.CondicionTributaria)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Cliente>> GetAllAsync()
    {
        return await DbContext.Set<Cliente>()
            .Include(c => c.CondicionTributaria)
            .ToListAsync();
    }
}
