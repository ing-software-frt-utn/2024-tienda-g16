using Domain.Aggregates.SesionAggregate;
using Domain.Aggregates.UserAggregate;

namespace Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Sesion sesion, User user);
}
