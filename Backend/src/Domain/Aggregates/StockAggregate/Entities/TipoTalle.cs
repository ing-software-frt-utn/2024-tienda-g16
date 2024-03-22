using Domain.Abstractions;

namespace Domain.Aggregates.StockAggregate.Entities;

public class TipoTalle : Entity
{
    private TipoTalle(Guid id, string descripcion) : base(id)
    {
        Descripcion = descripcion;
    }

    public string Descripcion { get; private set; }

# pragma warning disable CS8618
    private TipoTalle(Guid id) : base(id) { }
# pragma warning restore CS8618
}