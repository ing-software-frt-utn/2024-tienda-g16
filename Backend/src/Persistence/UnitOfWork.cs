using Domain.Abstractions;
using System.Data;

namespace Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TiendaDbContext _dbContext;

    public UnitOfWork(TiendaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
