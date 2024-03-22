using Domain.Aggregates.VentaAggregate.Entities;

namespace Domain.RepositoriesContracts;

public interface ICondicionTributariaRepository
{
    Task<CondicionTributaria?> GetByIdAsync(Guid id);
    Task<List<CondicionTributaria>> GetAllAsync();
}
