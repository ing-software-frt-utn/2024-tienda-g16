using Domain.Aggregates.UserAggregate;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(TiendaDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await DbContext.Users.Where(u => u.Email == email)
            .Include(u => u.Vendedor)
                .ThenInclude(v => v.Sucursal)
            .FirstOrDefaultAsync();
    }
    public override async Task<User?> GetByIdAsync(Guid id)
    {
        return await DbContext.Users.Where(u => u.Id == id)
            .Include(u => u.Vendedor)
            .FirstOrDefaultAsync();
    }
}
