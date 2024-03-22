using Domain.Abstractions;

namespace Domain.Aggregates.ArticuloAggregate.Entities;

public class Categoria : Entity
{
    private Categoria(Guid id, string description) : base(id)
    {
        Descripcion = description;
    }
    public string Descripcion { get; private set; }

# pragma warning disable CS8618
    private Categoria(Guid id) : base(id) { }
# pragma warning restore CS8618
}
