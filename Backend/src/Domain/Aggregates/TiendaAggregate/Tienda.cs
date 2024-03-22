using Domain.Abstractions;
using Domain.Aggregates.SucursalAggregate;
using Domain.Aggregates.VentaAggregate.Entities;

namespace Domain.Aggregates.TiendaAggregate;

public class Tienda : Entity
{
    private readonly List<Sucursal> _sucursales = [];
    private static readonly object _lockSync = new object();
    private static Tienda? _instance;
    private Tienda _tienda;
    private Tienda(Guid id) : base(id) { }
    public static Tienda Instance
    {
        get
        {
            lock (_lockSync)
            {
                return _instance ??= new Tienda(Guid.NewGuid());
            }
        }
    }
    public void SetTienda(Tienda tienda)
    {
        _tienda = tienda;
    }
    public Tienda GetTienda() => _tienda;
    public string Cuit { get; private set; }
    public virtual IReadOnlyList<Sucursal> Sucursales { get => _sucursales.ToList(); }
    public virtual CondicionTributaria CondicionTributaria { get; private set; }
}

