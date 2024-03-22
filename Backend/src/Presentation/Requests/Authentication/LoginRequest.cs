namespace Presentation.Requests.Authentication;

public record LoginRequest(
    string Email,
    string Password,
    string PuntoDeVentaId);
