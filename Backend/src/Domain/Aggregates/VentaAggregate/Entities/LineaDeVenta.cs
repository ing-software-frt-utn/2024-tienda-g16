using Domain.Abstractions;
using Domain.Aggregates.StockAggregate;
using Domain.Common.DomainExceptions.LineasDeVenta;

namespace Domain.Aggregates.VentaAggregate.Entities;

public class LineaDeVenta : Entity
{
    private LineaDeVenta(
        Guid id,
        Stock stock,
        int cantidad,
        double precioUnitario, 
        double importeIvaUnitario) : base(id)
    {
        Cantidad = cantidad;
        Stock = stock;
        PrecioUnitario = precioUnitario;
        ImporteIvaUnitario = importeIvaUnitario;
    }

    public int Cantidad { get; private set; }
    public double PrecioUnitario { get; private set; }
    public double ImporteIvaUnitario { get; private set; }
    public virtual Stock Stock { get; private set; }

    public void ModificarCantidad(int nuevaCantidad)
    {
        var cantidadActual = Cantidad;
        var aumento = nuevaCantidad - cantidadActual;

        if (Stock.CantidadDisponible < aumento)
        {
            throw new LineaDeVentaStockInsuficienteDomainException();
        }

        Cantidad = nuevaCantidad;

        if (aumento > 0)
        {
            Stock.ReservarCantidad(aumento);
        }
        else if (aumento < 0)
        {
            Stock.LiberarCantidad(-aumento);
        }
    }

    public void Cancelar()
    {
        Stock.LiberarCantidad(Cantidad);
    }

    public void Confirmar()
    {
        Stock.Confirmar(Cantidad);
    }

    public double GetImporteIva()
    {
        return Cantidad * ImporteIvaUnitario;
    }

    public double GetSubtotal()
    {
        return Cantidad * PrecioUnitario;
    }

    public double GetImporteNeto()
    {
        return Cantidad * (PrecioUnitario - ImporteIvaUnitario);
    }

    public static LineaDeVenta Create(
        Stock stock,
        int cantidad)
    {
        if (cantidad < 0)
        {
            throw new LineaDeVentaCantidadNegativaDomainException();
        }

        if (stock.CantidadDisponible < cantidad)
        {
            throw new LineaDeVentaStockInsuficienteDomainException();
        }

        stock.ReservarCantidad(cantidad);

        return new LineaDeVenta(
            Guid.NewGuid(),
            stock,
            cantidad,
            stock.GetPrecioUnitario(),
            stock.GetImporteIva());
    }

# pragma warning disable CS8618
    private LineaDeVenta(Guid id) : base(id) { }
# pragma warning restore CS8618
}
