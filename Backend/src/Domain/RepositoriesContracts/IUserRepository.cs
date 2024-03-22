using Domain.Aggregates.UserAggregate;

namespace Domain.RepositoriesContracts;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    void Add(User user);
    Task<User?> GetByIdAsync(Guid id);
}
