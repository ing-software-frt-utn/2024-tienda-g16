using ErrorOr;

namespace Domain.Common.Errors;

public static class SesionErrors
{
    public static Error VendedorDeDistintaSucursal => Error.Forbidden(
        "Login.VendedorDeDistintaSucursal",
        "El vendedor no pertenece a la sucursal");

    public static Error MasDeUnaSesionActiva => Error.Forbidden(
        "Login.MasDeUnaSesionActiva",
        "El vendedor o el punto de venta ya tienen una sesion activa diferente");

    public static Error SesionNoEncontrada => Error.NotFound(
        "Sesion.NoEncontrada",
        "No se pudo encontrar la sesion");

    public static Error SesionInvalidada => Error.Unauthorized(
        "Sesion.Cerrada",
        "Esta sesion ya fue cerrada");
}
