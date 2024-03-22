using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Abstractions;
using Domain.Aggregates.VentaAggregate.Entities;
using Domain.Common.Errors;
using Domain.GatewayContracts;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.AgregarPagoTarjeta;

public class AgregarPagoTarjetaCommandHandler : ICommandHandler<AgregarPagoTarjetaCommand, VentaResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IVentaRepository _ventaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAutorizacionPagoTarjetaGateway _tarjetaGateway;

    public AgregarPagoTarjetaCommandHandler(
        ISesionRepository sesionRepository,
        IVentaRepository ventaRepository,
        IUnitOfWork unitOfWork,
        IAutorizacionPagoTarjetaGateway tarjetaGateway)
    {
        _sesionRepository = sesionRepository;
        _ventaRepository = ventaRepository;
        _unitOfWork = unitOfWork;
        _tarjetaGateway = tarjetaGateway;
    }

    public async Task<ErrorOr<VentaResult>> Handle(
        AgregarPagoTarjetaCommand request,
        CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion is null) return SesionErrors.SesionInvalidada;

        var venta = await _ventaRepository.GetByIdWithAllAsync(new Guid(request.VentaId));
        if (venta is null) return VentaErrors.VentaNoEncontrada;

        var tarjeta = new Tarjeta(
            request.NumeroTarjeta,
            request.MesExpiracion,
            request.AnioExpiracion,
            request.CodigoDeSeguridad,
            request.NombreTitular,
            request.ApellidoTitular,
            request.Dni);

        try
        {
            await venta.RealizarPagoConTarjeta(tarjeta, _tarjetaGateway);
        }
        catch (DomainException exc)
        {
            return exc.Error;
        }

        await _unitOfWork.SaveChangesAsync();

        return new VentaResult(VentaDTO.ToDTO(venta));
    }
}


