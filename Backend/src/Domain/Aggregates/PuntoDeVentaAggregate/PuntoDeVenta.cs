using Domain.Abstractions;
using Domain.Aggregates.SucursalAggregate;
using Domain.Aggregates.VentaAggregate;

namespace Domain.Aggregates.PuntoDeVentaAggregate;

public class PuntoDeVenta : AggregateRoot
{
    private readonly List<Venta> _ventas = [];
    private PuntoDeVenta(
        Guid id,
        Sucursal sucursal,
        int numero) : base(id)
    {
        Sucursal = sucursal;
        Numero = numero;
    }

    public Sucursal Sucursal { get; private set; }
    public int Numero { get; private set; }

    public virtual IReadOnlyList<Venta> Ventas { get => _ventas.ToList(); }

# pragma warning disable CS8618
    private PuntoDeVenta(Guid id) : base(id) { }
# pragma warning restore CS8618
}
