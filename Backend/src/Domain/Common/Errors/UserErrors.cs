using ErrorOr;

namespace Domain.Common.Errors;

public static class UserErrors
{
    public static Error UsuarioNoEncontrado => Error.NotFound(
        "Usuario.NoEncontrado",
        "No se pudo encontrar el usuario");

    public static Error CredencialesIncorrectas => Error.Unauthorized(
        "Usuario.CredencialesIncorrectas",
        "El correo y/o la clave no son correctos");

    public static Error EmailEnUso => Error.Forbidden(
        "User.EmailEnUso",
        "El email ya está en uso");
}
