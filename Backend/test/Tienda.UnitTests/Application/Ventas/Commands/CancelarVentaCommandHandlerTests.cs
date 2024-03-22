using Application.AppServices.Ventas.Commands.CancelarVenta;
using Domain.Abstractions;
using Domain.Aggregates.SesionAggregate;
using Domain.Aggregates.VentaAggregate;
using Domain.RepositoriesContracts;
using FluentAssertions;
using Moq;

namespace Tienda.UnitTests.Application.Ventas.Commands;

public class CancelarVentaCommandHandlerTests
{
    private readonly Mock<ISesionRepository> _sesionRepositoryMock;
    private readonly Mock<IVentaRepository> _ventaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CancelarVentaCommandHandlerTests()
    {
        _sesionRepositoryMock = new();
        _ventaRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Deberia_LlamarRemoverDeVentaRepository()
    {
        // Arrange
        var command = new CancelarVentaCommand("", "");

        var venta = new Venta();
        var sesion = new Sesion();

        _sesionRepositoryMock.Setup(x =>
            x.GetByIdAsync(
                It.IsAny<Guid>())
            ).ReturnsAsync(sesion);

        _ventaRepositoryMock.Setup(x =>
            x.GetByIdWithAllAsync(
                It.IsAny<Guid>())
            ).ReturnsAsync(venta);

        var handler = new CancelarVentaCommandHandler(
            _sesionRepositoryMock.Object,
            _ventaRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeFalse();
        _ventaRepositoryMock.Verify(x => x.Remove(It.IsAny<Venta>()),
            Times.Once);
    }
}
