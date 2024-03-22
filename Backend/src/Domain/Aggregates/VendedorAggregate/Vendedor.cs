using Domain.Abstractions;
using Domain.Aggregates.SucursalAggregate;
using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.VentaAggregate;

namespace Domain.Aggregates.VendedorAggregate;

public class Vendedor : Entity
{
    internal readonly List<Venta> _ventas = [];
    internal readonly List<User> _users = [];
    internal Vendedor(
        Guid id,
        string legajo,
        string nombre,
        string apellido) : base(id)
    {
        Legajo = legajo;
        Nombre = nombre;
        Apellido = apellido;
    }

    public string Legajo { get; internal set; }
    public string Nombre { get; internal set; }
    public string Apellido { get; internal set; }

    public virtual IReadOnlyList<User> Users { get => _users.ToList(); }
    public virtual IReadOnlyList<Venta> Ventas { get => _ventas.ToList(); }
    public virtual Sucursal? Sucursal { get; internal set; }

#pragma warning disable CS8618
    internal Vendedor() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618
}
