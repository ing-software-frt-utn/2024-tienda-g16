using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Queries.ObtenerVentaActual;

public class ObtenerVentaActualQueryHandler : IQueryHandler<ObtenerVentaActualQuery, VentaResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IVentaRepository _ventaRepository;

    public ObtenerVentaActualQueryHandler(
        ISesionRepository sesionRepository,
        IVentaRepository ventaRepository)
    {
        _sesionRepository = sesionRepository;
        _ventaRepository = ventaRepository;
    }

    public async Task<ErrorOr<VentaResult>> Handle(ObtenerVentaActualQuery request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null)
        {
            return SesionErrors.SesionInvalidada;
        }

        var vendedor = sesion.Vendedor;
        var puntoDeVenta = sesion.PuntoDeVenta;

        var venta = await _ventaRepository.GetByVendedorAndPuntoDeVentaWithAllAsync(vendedor, puntoDeVenta);
        if (venta == null)
        {
            return VentaErrors.VentaNoEncontrada;
        }

        return new VentaResult(VentaDTO.ToDTO(venta));
    }
}
