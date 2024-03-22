using Application.AppServices.Ventas.Commands.ModificarLineaDeVenta;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using FluentAssertions;
using Moq;

namespace Tienda.UnitTests.Application.Ventas.Commands;

public class ModificarLineaDeVentaCommandHandlerTests
{
    private readonly Mock<IVentaRepository> _ventaRepositoryMock;
    private readonly Mock<ISesionRepository> _sesionRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public ModificarLineaDeVentaCommandHandlerTests()
    {
        _ventaRepositoryMock = new();
        _sesionRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Deberia_DevolverError_CuandoSesionNoExiste()
    {
        // Arrange
        var command = new ModificarLineaDeVentaCommand("", "", "", 0);

        _sesionRepositoryMock.Setup(x =>
            x.GetByIdAsync(
                It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        var handler = new ModificarLineaDeVentaCommandHandler(
            _ventaRepositoryMock.Object,
            _sesionRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(SesionErrors.SesionInvalidada);
    }

    [Fact]
    public async Task Handle_NoDeberia_LlamarUnitOfWork_CuandoSucedeError()
    {
        // Arrange
        var command = new ModificarLineaDeVentaCommand("", "", "", 0);

        _sesionRepositoryMock.Setup(x =>
            x.GetByIdAsync(
                It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        var handler = new ModificarLineaDeVentaCommandHandler(
            _ventaRepositoryMock.Object,
            _sesionRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsError.Should().BeTrue();
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
