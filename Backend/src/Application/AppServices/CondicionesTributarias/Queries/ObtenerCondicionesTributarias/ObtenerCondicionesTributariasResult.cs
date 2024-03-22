using Application.DTOs.Common;

namespace Application.AppServices.CondicionesTributarias.Queries.ObtenerCondicionesTributarias;

public sealed record ObtenerCondicionesTributariasResult(
    List<CondicionTributariaDTO?> CondicionesTributarias);
