using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using Application.ExtensionMethods;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.ModificarLineaDeVenta;

public class ModificarLineaDeVentaCommandHandler : ICommandHandler<ModificarLineaDeVentaCommand, VentaResult>
{
    private readonly IVentaRepository _ventaRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ModificarLineaDeVentaCommandHandler(
        IVentaRepository ventaRepository,
        ISesionRepository sesionRepository,
        IUnitOfWork unitOfWork)
    {
        _ventaRepository = ventaRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<VentaResult>> Handle(ModificarLineaDeVentaCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(request.SesionId.ToGuidOrDefault());
        if (sesion == null)
        {
            return SesionErrors.SesionInvalidada;
        }

        var venta = await _ventaRepository.GetByIdWithAllAsync(request.VentaId.ToGuidOrDefault());
        if (venta == null)
        {
            return VentaErrors.VentaNoEncontrada;
        }

        if (venta.Vendedor != sesion.Vendedor)
        {
            return VentaErrors.EsDeOtroVendedor;
        }

        if (venta.PuntoDeVenta != sesion.PuntoDeVenta)
        {
            return VentaErrors.EsDeOtroPuntoDeVenta;
        }

        try
        {
            venta.ModificarLineaDeVenta(new Guid(request.LineaDeVentaId), request.Cantidad);
        }
        catch (DomainException exc)
        {
            return exc.Error;
        }

        await _unitOfWork.SaveChangesAsync();

        return new VentaResult(VentaDTO.ToDTO(venta));
    }
}
