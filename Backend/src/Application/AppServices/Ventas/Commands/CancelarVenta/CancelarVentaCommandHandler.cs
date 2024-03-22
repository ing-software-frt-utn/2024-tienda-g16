using Application.Abstractions.Messaging;
using Application.ExtensionMethods;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.CancelarVenta;

public class CancelarVentaCommandHandler : ICommandHandler<CancelarVentaCommand>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IVentaRepository _ventaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelarVentaCommandHandler(
        ISesionRepository sesionRepository,
        IVentaRepository ventaRepository,
        IUnitOfWork unitOfWork)
    {
        _sesionRepository = sesionRepository;
        _ventaRepository = ventaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(CancelarVentaCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(request.SesionId.ToGuidOrDefault());
        if (sesion is null) return SesionErrors.SesionInvalidada;

        var venta = await _ventaRepository.GetByIdWithAllAsync(request.VentaId.ToGuidOrDefault());
        if (venta is null) return VentaErrors.VentaNoEncontrada;

        try
        {
            venta.Cancelar(sesion.Vendedor);
        }
        catch (DomainException exc)
        {
            return exc.Error;
        }

        _ventaRepository.Remove(venta);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}
