using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal abstract class Repository<TEntity>
    where TEntity : Entity
{
    protected readonly TiendaDbContext DbContext;
    protected Repository(TiendaDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public virtual Task<TEntity?> GetByIdAsync(Guid id)
    {
        return DbContext.Set<TEntity>()
            .Where(e => e.Id.Equals(id)).SingleOrDefaultAsync();
    }
}
