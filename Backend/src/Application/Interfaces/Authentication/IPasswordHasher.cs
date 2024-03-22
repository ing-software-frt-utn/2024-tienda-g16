namespace Application.Interfaces.Authentication;

public interface IPasswordHasher
{
    string Hash(string password);

    public bool Verify(string passwordHash, string inputPassword);
}
