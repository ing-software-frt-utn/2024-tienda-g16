using Application.Abstractions.Messaging;
using ErrorOr;

namespace Application.AppServices.Authentication.Queries.Sesiones;

public sealed record GetSesionesActivasQuery() : IQuery<GetSesionesActivasResult>;
