using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RemoteServicesContracts;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.ConfirmarVenta;

public class ConfirmarVentaCommandHandler : ICommandHandler<ConfirmarVentaCommand, VentaResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IVentaRepository _ventaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAutorizacionAfipGateway _gateway;

    public ConfirmarVentaCommandHandler(
        ISesionRepository sesionRepository,
        IVentaRepository ventaRepository,
        IUnitOfWork unitOfWork,
        IAutorizacionAfipGateway gateway)
    {
        _sesionRepository = sesionRepository;
        _ventaRepository = ventaRepository;
        _unitOfWork = unitOfWork;
        _gateway = gateway;
    }
    public async Task<ErrorOr<VentaResult>> Handle(ConfirmarVentaCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion is null) return SesionErrors.SesionInvalidada;

        var venta = await _ventaRepository.GetByIdWithAllAsync(new Guid(request.VentaId));
        if (venta is null) return VentaErrors.VentaNoEncontrada;

        try
        {
            await venta.Confirmar(sesion.Vendedor, _gateway);
        }
        catch (DomainException exc)
        {
            return exc.Error;
        }

        await _unitOfWork.SaveChangesAsync();

        return new VentaResult(VentaDTO.ToDTO(venta));
    }
}
