using Domain.Abstractions;
using Domain.Primitives;

namespace Domain.Aggregates.VentaAggregate.Entities;

public class TipoDeComprobante : Entity
{
    private readonly List<SeEmitePara> _seEmitePara = [];
    private readonly List<EsEmitidoPor> _esEmitidoPor = [];
    private TipoDeComprobante(
        Guid id,
        ComprobanteTipo tipoComprobante) : base(id)
    {
        ComprobanteTipo = tipoComprobante;
    }

    public string Descripcion { get => ComprobanteTipo.ToString(); }
    public ComprobanteTipo ComprobanteTipo { get; private set; }

    public virtual IReadOnlyList<SeEmitePara> SeEmitePara { get => _seEmitePara.ToList(); }
    public virtual IReadOnlyList<EsEmitidoPor> EsEmitidoPor { get => _esEmitidoPor.ToList(); }

#pragma warning disable CS8618
    private TipoDeComprobante(Guid id) : base(id) { }
# pragma warning restore CS8618
}
