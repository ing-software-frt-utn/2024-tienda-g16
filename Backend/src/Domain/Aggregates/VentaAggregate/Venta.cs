using Domain.Abstractions;
using Domain.Aggregates.ClienteAggregate;
using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.Aggregates.StockAggregate;
using Domain.Aggregates.VendedorAggregate;
using Domain.Aggregates.VentaAggregate.Entities;
using Domain.Aggregates.VentaAggregate.ValueObjects;
using Domain.Common.DomainExceptions.Ventas;
using Domain.DomainEvents;
using Domain.GatewayContracts;
using Domain.RemoteServicesContracts;

namespace Domain.Aggregates.VentaAggregate;

public class Venta : AggregateRoot
{
    private readonly List<LineaDeVenta> _lineasDeVenta = [];
    internal Venta(
        Guid id,
        PuntoDeVenta puntoDeVenta,
        Vendedor vendedor) : base(id)
    {
        PuntoDeVenta = puntoDeVenta;
        Vendedor = vendedor;
        CreadaUtc = DateTime.UtcNow;
    }
    public DateTime? CreadaUtc { get; internal set; }
    public bool Confirmada { get; internal set; } = false;
    public DateTime? ConfirmadaUtc { get; internal set; }
    public CodigoComprobante? CodigoComprobante { get; internal set; }
    public IReadOnlyList<LineaDeVenta> LineasDeVenta { get => _lineasDeVenta.ToList(); }
    public PuntoDeVenta PuntoDeVenta { get; internal set; }
    public Vendedor Vendedor { get; internal set; }
    public double Total { get => _lineasDeVenta.Any() ? _lineasDeVenta.Sum(ldv => ldv.GetSubtotal()) : 0; }
    public double ImporteIva { get => _lineasDeVenta.Any() ? _lineasDeVenta.Sum(ldv => ldv.GetImporteIva()) : 0; }
    public double ImporteNeto { get => _lineasDeVenta.Any() ? _lineasDeVenta.Sum(ldv => ldv.GetImporteNeto()) : 0; }

    public virtual Cliente? Cliente { get; internal set; }
    public virtual TipoDeComprobante? TipoDeComprobante { get; internal set; }
    public virtual Pago? Pago { get; internal set; }

    public static Venta Create(PuntoDeVenta puntoDeVenta, Vendedor vendedor)
    {
        var venta = new Venta(
            Guid.NewGuid(),
            puntoDeVenta,
            vendedor);

        return venta;
    }

    public void AgregarArticulo(Stock stock, int cantidad)
    {
        if (Confirmada)
        {
            throw new VentaNoSePuedeModificarConfirmadaDomainException();
        }

        if (stock.Sucursal != Vendedor.Sucursal)
        {
            throw new VentaStockDeOtraSucursalDomainException();
        }

        var lineaDeVentaDuplicada = _lineasDeVenta.FirstOrDefault(ldv => ldv.Stock == stock);

        if (lineaDeVentaDuplicada != null)
        {
            lineaDeVentaDuplicada.ModificarCantidad(lineaDeVentaDuplicada.Cantidad + cantidad);
        }
        else
        {
            var lineaDeVenta = LineaDeVenta.Create(stock, cantidad);
            _lineasDeVenta.Add(lineaDeVenta);
        }
    }
    public void AsociarCliente(Cliente cliente)
    {
        if (Confirmada)
        {
            throw new VentaNoSePuedeModificarConfirmadaDomainException();
        }

        if (Cliente == cliente)
        {
            throw new VentaNoSePuedeModificarConfirmadaDomainException();
        }

        Cliente = cliente;
        RaiseDomainEvent(new VentaClienteAssociatedDomainEvent(this, cliente));
    }

    public void AsociarTipoComprobante(TipoDeComprobante tipoDeComprobante)
    {
        TipoDeComprobante = tipoDeComprobante;
    }

    public void ModificarLineaDeVenta(Guid lineaDeVentaId, int nuevaCantidad)
    {
        var lineaDeVenta = LineasDeVenta.FirstOrDefault(ldv => ldv.Id == lineaDeVentaId);

        if (lineaDeVenta == null)
        {
            throw new VentaNoTieneLaLineaDeVentaDomainException();
        }

        lineaDeVenta.ModificarCantidad(nuevaCantidad);

        if (nuevaCantidad == 0)
        {
            _lineasDeVenta.Remove(lineaDeVenta);
        }
    }

    public void RealizarPagoEnEfectivo(double montoAbonado)
    {
        if (_lineasDeVenta.Count == 0)
        {
            throw new VentaNoSePuedePagarVaciaDomainException();
        }

        if (Pago != null)
        {
            throw new VentaYaTienePagoAsociadoDomainException();
        }

        if (Total > montoAbonado)
        {
            throw new VentaPagoMontoInsuficienteDomainException();
        }

        Pago = Pago.PagoEnEfectivo(montoAbonado, Total);
    }

    public async Task RealizarPagoConTarjeta(Tarjeta tarjeta, IAutorizacionPagoTarjetaGateway gateway)
    {
        if (_lineasDeVenta.Count == 0)
        {
            throw new VentaNoSePuedePagarVaciaDomainException();
        }

        if (Pago != null)
        {
            throw new VentaYaTienePagoAsociadoDomainException();
        }

        await gateway.AutorizarPagoTarjeta(tarjeta, this);

        Pago = Pago.PagoConTarjeta(monto: Total);
    }

    public void Cancelar(Vendedor vendedor)
    {
        if (Vendedor != vendedor)
        {
            throw new VentaEsDeOtroVendedorDomainException();
        }

        if (Confirmada)
        {
            throw new VentaNoSePuedeModificarConfirmadaDomainException();
        }

        if (Pago is not null)
        {
            throw new VentaNoSePuedeCancelarPagadaDomainException();
        }

        _lineasDeVenta.ForEach(ldv => ldv.Cancelar());
        _lineasDeVenta.Clear();
    }

    public async Task Confirmar(Vendedor vendedor, IAutorizacionAfipGateway gateway)
    {
        if (Vendedor != vendedor)
        {
            throw new VentaEsDeOtroVendedorDomainException();
        }

        if (Confirmada)
        {
            throw new VentaYaConfirmadaDomainException();
        }

        if (Pago == null)
        {
            throw new VentaNoPagadaDomainException();
        }

        var montoMaximo = gateway.ObtenerMontoMaximoConsumidorFinal();
        if (Total > montoMaximo && Cliente!.IsClientePorDefecto())
        {
            throw new VentaNoPuedeSerAnonimaDomainException();
        }

        CodigoComprobante = await gateway.AutorizarVenta(this);
        Confirmada = true;
        ConfirmadaUtc = DateTime.UtcNow;
        _lineasDeVenta.ForEach(ldv => ldv.Confirmar());
    }

#pragma warning disable CS8618
    internal Venta(Guid id) : base(id) { }
    internal Venta() : base(Guid.NewGuid()) { }
# pragma warning restore CS8618
}
