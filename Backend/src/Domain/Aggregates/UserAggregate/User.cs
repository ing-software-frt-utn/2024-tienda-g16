using Domain.Abstractions;
using Domain.Aggregates.VendedorAggregate;

namespace Domain.Aggregates.UserAggregate;

public class User : Entity
{
    private User(
        Guid id,
        Vendedor vendedor,
        string email,
        string password) : base(id)
    {
        Email = email;
        Password = password;
        Vendedor = vendedor;
    }

    public string Email { get; private set; }
    public string Password { get; private set; }
    public Vendedor Vendedor { get; private set; }

    public static User Create(
        Vendedor vendedor,
        string email,
        string password)
    {
        return new User(
            Guid.NewGuid(),
            vendedor,
            email,
            password);
    }

    # pragma warning disable CS8618
    private User(Guid id) : base(id) { }
    # pragma warning restore CS8618
}
