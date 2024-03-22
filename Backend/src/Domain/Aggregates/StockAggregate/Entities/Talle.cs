using Domain.Abstractions;

namespace Domain.Aggregates.StockAggregate.Entities;

public class Talle : Entity
{
    private Talle(
        Guid id,
        TipoTalle tipoTalle) : base(id)
    {
        TipoTalle = tipoTalle;
    }
    public double Medida { get; private set; }
    public TipoTalle TipoTalle { get; private set; }

# pragma warning disable CS8618
    private Talle(Guid id) : base(id) { }
# pragma warning restore CS8618
}
