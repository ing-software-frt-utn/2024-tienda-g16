using Domain.Aggregates.ArticuloAggregate;
using Domain.Aggregates.StockAggregate;
using Domain.Aggregates.SucursalAggregate;
using Domain.Aggregates.VendedorAggregate;
using Domain.Aggregates.VentaAggregate;
using Domain.Common.DomainExceptions.Ventas;
using FluentAssertions;

namespace Tienda.UnitTests.Domain.Entities;

public class VentaTest
{

    [Fact]
    public void AgregarArticulo_Deberia_LevantarExcepcion_CuandoVentaYaEstaConfirmada()
    {
        // Arrange
        var venta = new Venta()
        {
            Confirmada = true,
        };

        var stock = new Stock();

        // Act and Assert
        Assert.Throws<VentaNoSePuedeModificarConfirmadaDomainException>(
            () => venta.AgregarArticulo(stock, 1));
    }

    [Fact]
    public void AgregarArticulo_Deberia_AgregarLineaDeVenta_CuandoNoSeaRepetida()
    {
        // Arrange
        var sucursal = new Sucursal();

        var articulo = new Articulo()
        {
            PrecioFinal = 9.68,
            Iva = 0.21,
            NetoGravado = 8.0,
        };

        var stock = new Stock
        {
            Articulo = articulo,
            CantidadDisponible = 1,
            CantidadReservada = 0,
            Sucursal = sucursal,
        };

        var venta = new Venta()
        {
            Confirmada = false,
            Vendedor = new Vendedor() { 
                Sucursal = sucursal},
        };

        // Act
        venta.AgregarArticulo(stock, 1);

        // Assert
        venta.LineasDeVenta.Should().HaveCount(1);
        venta.LineasDeVenta.First().Stock.Should().Be(stock);
    }
}
