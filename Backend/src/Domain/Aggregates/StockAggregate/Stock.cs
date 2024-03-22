using Domain.Abstractions;
using Domain.Aggregates.ArticuloAggregate;
using Domain.Aggregates.StockAggregate.Entities;
using Domain.Aggregates.SucursalAggregate;
using Domain.Common.DomainExceptions.Stocks;
using ErrorOr;

namespace Domain.Aggregates.StockAggregate;

public class Stock : AggregateRoot
{
    internal Stock(
        Guid id,
        int cantidadDisponible,
        Talle talle,
        Color color,
        Articulo articulo,
        Sucursal sucursal
        ) : base(id)
    {
        CantidadDisponible = cantidadDisponible;
        Talle = talle;
        Color = color;
        Articulo = articulo;
        Sucursal = sucursal;
    }

    public int CantidadDisponible { get; internal set; }
    public int CantidadReservada { get; internal set; }
    public Talle Talle { get; internal set; }
    public Color Color { get; internal set; }
    public Articulo Articulo { get; internal set; }
    public Sucursal Sucursal { get; internal set; }

    public static ErrorOr<Stock> Create(
        int cantidadDisponible,
        Talle talle,
        Color color,
        Articulo articulo,
        Sucursal sucursal)
    {
        if (cantidadDisponible < 0)
        {
            throw new StockCantidadNegativaDomainException();
        }

        return new Stock(
            Guid.NewGuid(),
            cantidadDisponible,
            talle,
            color,
            articulo,
            sucursal);
    }

    public ErrorOr<Updated> ReservarCantidad(int cantidad)
    {
        if (cantidad > CantidadDisponible)
        {
            throw new StockNoSePuedeReservarDomainException();
        }

        CantidadReservada += cantidad;
        CantidadDisponible -= cantidad;

        return Result.Updated;
    }

    public ErrorOr<Updated> LiberarCantidad(int cantidad)
    {
        if (cantidad > CantidadReservada)
        {
            throw new StockNoSePuedeLiberarDomainException();
        }

        CantidadDisponible += cantidad;
        CantidadReservada -= cantidad;

        return Result.Updated;
    }

    public void Confirmar(int cantidad)
    {
        if (cantidad > CantidadReservada)
        {
            throw new StockNoSePuedeConfirmarDomainException();
        }

        CantidadReservada -= cantidad;
    }

    public double GetImporteIva()
    {
        return Articulo.GetImporteIva();
    }

    public double GetPrecioUnitario()
    {
        return Articulo.PrecioFinal;
    }

#pragma warning disable CS8618
    internal Stock(Guid id) : base(id) { }
    internal Stock() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618
}
