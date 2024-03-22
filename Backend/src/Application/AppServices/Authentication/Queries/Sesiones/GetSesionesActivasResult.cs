using Application.DTOs.Common;

namespace Application.AppServices.Authentication.Queries.Sesiones;

public sealed record GetSesionesActivasResult(
    List<SesionDTO?> Sesiones);
