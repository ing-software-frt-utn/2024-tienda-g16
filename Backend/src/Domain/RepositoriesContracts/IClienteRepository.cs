using Domain.Aggregates.ClienteAggregate;

namespace Domain.RepositoriesContracts;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(Guid id);
    Task<Cliente?> GetByNumeroDocumentoAsync(string numeroDocumento);
    Task<List<Cliente>> GetAllAsync();
    void Add(Cliente cliente);
}
