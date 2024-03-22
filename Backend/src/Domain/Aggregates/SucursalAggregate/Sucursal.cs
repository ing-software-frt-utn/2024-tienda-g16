using Domain.Abstractions;
using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.Aggregates.StockAggregate;
using Domain.Aggregates.TiendaAggregate;
using Domain.Aggregates.VendedorAggregate;

namespace Domain.Aggregates.SucursalAggregate;

public class Sucursal : Entity
{
    internal readonly List<Stock> _stocks = [];
    internal readonly List<PuntoDeVenta> _puntosDeVenta = [];
    internal readonly List<Vendedor> _vendedores = [];
    internal Sucursal(
        Guid id,
        Tienda tienda,
        string nombre) : base(id)
    {
        Nombre = nombre;
        Tienda = tienda;
    }

    public string Nombre { get; internal set; }
    public Tienda Tienda { get; internal set; }

    public virtual IReadOnlyList<PuntoDeVenta> PuntosDeVenta { get => _puntosDeVenta.ToList(); }
    public virtual IReadOnlyList<Stock> Stocks { get => _stocks.ToList(); }
    public virtual IReadOnlyList<Vendedor> Vendedores { get => _vendedores.ToList(); }

# pragma warning disable CS8618
    internal Sucursal(Guid id) : base(id) { }
    internal Sucursal() : base(Guid.NewGuid()) { }
# pragma warning restore CS8618
}
