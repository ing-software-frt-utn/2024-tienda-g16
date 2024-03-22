using Domain.Abstractions;

namespace Domain.Aggregates.ArticuloAggregate.Entities;

public class Marca : Entity
{
    private Marca(Guid id, string nombre) : base(id)
    {
        Nombre = nombre;
    }
    public string Nombre { get; private set; }

# pragma warning disable CS8618
    private Marca(Guid id) : base(id) { }
# pragma warning restore CS8618
}
