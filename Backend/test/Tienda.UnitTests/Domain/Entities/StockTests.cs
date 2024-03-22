using Domain.Aggregates.StockAggregate;
using Domain.Common.DomainExceptions.Stocks;
using FluentAssertions;

namespace Tienda.UnitTests.Domain.Entities;

public class StockTests
{
    [Fact]
    public void Confirmar_Deberia_LevantarUnaExcepcion_SiNoHayReservasAConfirmar()
    {
        // Arrange
        var stock = new Stock()
        {
            CantidadReservada = 0,
        };

        // Act and Assert
        Assert.Throws<StockNoSePuedeConfirmarDomainException>(
            () => stock.Confirmar(1));
        stock.CantidadReservada.Should().Be(0);
    }
}
