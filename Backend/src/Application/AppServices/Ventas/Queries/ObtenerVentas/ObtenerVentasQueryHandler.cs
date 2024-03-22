using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Queries.ObtenerVentas;

public class ObtenerVentasQueryHandler : IQueryHandler<ObtenerVentasQuery, ObtenerVentasResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IVentaRepository _ventaRepository;

    public ObtenerVentasQueryHandler(
        ISesionRepository sesionRepository,
        IVentaRepository ventaRepository)
    {
        _sesionRepository = sesionRepository;
        _ventaRepository = ventaRepository;
    }
    public async Task<ErrorOr<ObtenerVentasResult>> Handle(
        ObtenerVentasQuery request,
        CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null)
        {
            return SesionErrors.SesionInvalidada;
        }

        var ventas = await _ventaRepository.GetAllAsync();

        return new ObtenerVentasResult(ventas.Select(v => VentaDTO.ToDTO(v)).ToList());
    }
}
