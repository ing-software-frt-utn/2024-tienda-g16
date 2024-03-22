using Domain.Abstractions;

namespace Domain.Aggregates.VentaAggregate.Entities;

public class SeEmitePara : Entity
{
    private SeEmitePara(Guid id,
        TipoDeComprobante tipoDeComprobante,
        CondicionTributaria condicionTributaria) : base(id)
    {
        TipoDeComprobante = tipoDeComprobante;
        CondicionTributaria = condicionTributaria;
    }
    public virtual TipoDeComprobante TipoDeComprobante { get; private set; }
    public virtual CondicionTributaria CondicionTributaria { get; private set;  }

# pragma warning disable CS8618
    private SeEmitePara(Guid id) : base(id) { }
# pragma warning restore CS8618
}
