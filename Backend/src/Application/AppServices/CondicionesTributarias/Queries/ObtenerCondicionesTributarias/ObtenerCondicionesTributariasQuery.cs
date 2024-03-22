using Application.Abstractions.Messaging;

namespace Application.AppServices.CondicionesTributarias.Queries.ObtenerCondicionesTributarias;

public sealed record ObtenerCondicionesTributariasQuery(
    string SesionId) : IQuery<ObtenerCondicionesTributariasResult>;
