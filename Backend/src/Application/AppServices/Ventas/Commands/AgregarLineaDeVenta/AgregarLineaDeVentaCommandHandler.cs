using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Application.ExtensionMethods;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.AgregarLineaDeVenta;

public class AgregarLineaDeVentaCommandHandler : ICommandHandler<AgregarLineaDeVentaCommand, VentaResult>
{
    private readonly IVentaRepository _ventaRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IArticuloRepository _articuloRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AgregarLineaDeVentaCommandHandler(
        IVentaRepository ventaRepository,
        ISesionRepository sesionRepository,
        IUnitOfWork unitOfWork,
        IArticuloRepository articuloRepository)
    {
        _ventaRepository = ventaRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
        _articuloRepository = articuloRepository;
    }

    public async Task<ErrorOr<VentaResult>> Handle(AgregarLineaDeVentaCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(request.SesionId.ToGuidOrDefault());
        if (sesion == null) return SesionErrors.SesionInvalidada;
        
        var articulo = await _articuloRepository.GetByCodigoArticuloAsync(request.CodigoArticulo);
        if (articulo == null) return ArticuloErrors.ArticuloNoEncontrado;
            
        var stock = articulo.Stocks.FirstOrDefault(s => s.Id == request.StockId.ToGuidOrDefault());
        if (stock == null) return StockErrors.StockNoEncontrado;

        var venta = await _ventaRepository.GetByIdWithAllAsync(request.VentaId.ToGuidOrDefault());
        if (venta == null) return VentaErrors.VentaNoEncontrada;

        try
        { 
            venta.AgregarArticulo(stock, request.Cantidad);
        }
        catch(DomainException exc)
        {
            return exc.Error;
        }

        await _unitOfWork.SaveChangesAsync();

        var result = new VentaResult(VentaDTO.ToDTO(venta));
        return result;
    }
}
