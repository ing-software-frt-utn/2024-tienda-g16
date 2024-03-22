using Domain.Aggregates.StockAggregate;

namespace Domain.RepositoriesContracts;

public interface IStockRepository
{
    Task<Stock?> GetByIdAsync(Guid id);
}
