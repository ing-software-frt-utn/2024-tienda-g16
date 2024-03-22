namespace Presentation.Requests.Authentication;

public record RegisterRequest(
    string Email,
    string Password, 
    string VendedorId);
