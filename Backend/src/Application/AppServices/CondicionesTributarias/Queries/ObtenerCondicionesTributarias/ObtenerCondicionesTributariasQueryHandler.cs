using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.CondicionesTributarias.Queries.ObtenerCondicionesTributarias;

public class ObtenerCondicionesTributariasQueryHandler : IQueryHandler<ObtenerCondicionesTributariasQuery, ObtenerCondicionesTributariasResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly ICondicionTributariaRepository _condicionTributariaRepository;

    public ObtenerCondicionesTributariasQueryHandler(
        ISesionRepository sesionRepository,
        ICondicionTributariaRepository condicionTributariaRepository)
    {
        _sesionRepository = sesionRepository;
        _condicionTributariaRepository = condicionTributariaRepository;
    }

    public async Task<ErrorOr<ObtenerCondicionesTributariasResult>> Handle(
        ObtenerCondicionesTributariasQuery request,
        CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null) return SesionErrors.SesionInvalidada;

        var condicionesTributarias = await _condicionTributariaRepository.GetAllAsync();

        var result = new ObtenerCondicionesTributariasResult(
            condicionesTributarias.Select(ctt => CondicionTributariaDTO.ToDTO(ctt)).ToList());

        return result;
    }
}
