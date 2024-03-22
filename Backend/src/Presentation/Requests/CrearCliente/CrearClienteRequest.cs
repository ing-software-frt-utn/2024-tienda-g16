namespace Presentation.Requests.CrearCliente;

public sealed record CrearClienteRequest(
    string Nombre,
    int TipoDocumento,
    string Numero,
    string Apellido,
    string CondicionTributariaId);
