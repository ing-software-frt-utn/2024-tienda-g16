using Domain.Abstractions;
using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.Aggregates.SucursalAggregate;
using Domain.Aggregates.VendedorAggregate;

namespace Domain.Aggregates.SesionAggregate;

public class Sesion : Entity
{
    internal Sesion(
        Guid id,
        PuntoDeVenta puntoDeVenta,
        Vendedor vendedor) : base(id)
    {
        PuntoDeVenta = puntoDeVenta;
        Vendedor = vendedor;
    }
    public PuntoDeVenta PuntoDeVenta { get; internal set; }
    public Vendedor Vendedor { get; internal set; }

    public static Sesion Create(
        PuntoDeVenta puntoDeVenta, 
        Vendedor vendedor)
    {
        return new Sesion(
            Guid.NewGuid(),
            puntoDeVenta,
            vendedor);
    } 

    public Sucursal GetSucursal()
    {
        return PuntoDeVenta.Sucursal;
    }

# pragma warning disable CS8618
    internal Sesion(Guid id) : base(id) { }
    internal Sesion() : base(Guid.NewGuid()) { }
# pragma warning restore CS8618
}
