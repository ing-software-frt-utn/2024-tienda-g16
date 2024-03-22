using ErrorOr;

namespace Domain.Common.Errors;

public static class ClienteErrors
{
    public static Error ClientePorDefectoNoEncontrado => Error.Failure(
        "Cliente.ClientePorDefectoNoEncontrado",
        "Fatal: no se pudo encontrar al cliente por defecto");

    public static Error ClienteNoEncontrado => Error.Failure(
        "Cliente.ClienteNoEncontrado",
        "No se pudo encontrar el cliente");
}
