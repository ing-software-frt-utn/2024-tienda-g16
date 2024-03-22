using Domain.Abstractions;
using Domain.Aggregates.ArticuloAggregate.Entities;
using Domain.Aggregates.StockAggregate;
using Domain.Aggregates.SucursalAggregate;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Aggregates.ArticuloAggregate;

public class Articulo : AggregateRoot
{
    internal readonly List<Stock> _stocks = [];
    internal Articulo(
        Guid Id,
        string codigoArticulo,
        Marca marca,
        Categoria categoria,
        string descripcion,
        double iva,
        double costo,
        double margenDeGanancia,
        double netoGravado,
        double precioFinal) : base(Id)
    {
        CodigoArticulo = codigoArticulo;
        Marca = marca;
        Categoria = categoria;
        Descripcion = descripcion;
        Iva = iva;
        Costo = costo;
        MargenDeGanancia = margenDeGanancia;
        NetoGravado = netoGravado;
        PrecioFinal = precioFinal;
    }

    public string CodigoArticulo { get; internal set; }
    public Marca Marca { get; internal set; }
    public Categoria Categoria { get; internal set; }
    public string Descripcion { get; internal set; }
    public double Costo { get; internal set; }
    public double Iva { get; internal set; }
    public double MargenDeGanancia { get; internal set; }
    public double NetoGravado { get; internal set; }
    public double PrecioFinal { get; internal set; }

    public double GetImporteIva()
    {
        return PrecioFinal - NetoGravado;
    }


    public virtual IReadOnlyList<Stock> Stocks { get => _stocks.ToList(); }

    public static ErrorOr<Articulo> Create(
        string itemCode,
        Marca marca,
        Categoria categoria,
        string descripcion,
        double costo,
        double iva,
        double margenDeGanancia)
    {

        if (costo <= 0)
        {
            return ArticuloErrors.CostoInvalido;
        }

        double netoGravado = CalculateNetoGravado(costo, margenDeGanancia);
        double precioFinal = CalculatePrecioFinal(netoGravado, iva);

        return new Articulo(
            Guid.NewGuid(),
            itemCode,
            marca,
            categoria,
            descripcion,
            iva,
            costo,
            margenDeGanancia,
            netoGravado,
            precioFinal);
    }

    private static double CalculatePrecioFinal(double netoGravado, double iva)
    {
        return netoGravado * (1 + iva);
    }

    private static double CalculateNetoGravado(double costo, double margenDeGanancia)
    {
        return costo * (1 + margenDeGanancia);
    }

    public List<Stock> GetStockEnSucursal(Sucursal sucursal)
    {
        return _stocks.Where(s => s.Sucursal == sucursal).ToList();
    }

#pragma warning disable CS8618
    private Articulo(Guid id) : base(id) { }
    internal Articulo() : base(Guid.NewGuid()) { }
# pragma warning restore CS8618

}
