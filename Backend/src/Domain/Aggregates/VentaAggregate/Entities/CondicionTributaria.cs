using Domain.Abstractions;
using Domain.Primitives;

namespace Domain.Aggregates.VentaAggregate.Entities;

public class CondicionTributaria : Entity
{
    private CondicionTributaria(Guid id,
        TipoCondicionTibutaria tipo,
        string descripcion) : base(id)
    {
        Tipo = tipo;
        Descripcion = descripcion;
    }

    public TipoCondicionTibutaria Tipo { get; private set; }
    public string Descripcion { get; private set; }

# pragma warning disable CS8618
    private CondicionTributaria(Guid id) : base(id) { }
# pragma warning restore CS8618
}
