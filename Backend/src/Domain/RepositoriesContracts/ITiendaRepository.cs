using Domain.Aggregates.TiendaAggregate;

namespace Domain.RepositoriesContracts;

public interface ITiendaRepository
{
    Tienda? GetTienda();
}
