using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Authentication.Queries.Sesiones;

public class GetSesionesActivasQueryHandler : IQueryHandler<GetSesionesActivasQuery, GetSesionesActivasResult>
{
    private readonly ISesionRepository _sesionRepository;

    public GetSesionesActivasQueryHandler(ISesionRepository sesionRepository)
    {
        _sesionRepository = sesionRepository;
    }

    public async Task<ErrorOr<GetSesionesActivasResult>> Handle(GetSesionesActivasQuery request, CancellationToken cancellationToken)
    {
        var sesiones = await _sesionRepository.GetAllAsync();

        var sesionesDTO = sesiones.Select(s => SesionDTO.ToDTO(s)).ToList();

        return new GetSesionesActivasResult(sesionesDTO);
    }
}
