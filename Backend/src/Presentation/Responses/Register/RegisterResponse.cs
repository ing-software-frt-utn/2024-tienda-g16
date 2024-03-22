namespace Presentation.Responses.Register;

public sealed record RegisterResponse(
    RegisterResponseUser? User,
    RegisterResponseVendedor? Vendedor);

public sealed record RegisterResponseUser(
    string Email);

public sealed record RegisterResponseVendedor(
    string Id,
    string Legajo,
    string Nombre, 
    string Apellido);