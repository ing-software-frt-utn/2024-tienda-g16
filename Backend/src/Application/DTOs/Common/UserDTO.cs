using Domain.Aggregates.UserAggregate;

namespace Application.DTOs.Common;

public class UserDTO
{
    public string Email { get; private set; } = string.Empty;

    public static UserDTO? ToDTO(User? user)
    {
        if (user == null) return null;

        return new UserDTO
        {
            Email = user.Email,
        };
    }
}
