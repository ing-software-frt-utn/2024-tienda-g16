using ErrorOr;

namespace Domain.Common.Errors;

public static class CondicionTributariaErrors
{
    public static Error CondicionTributariaNoEncontrada => Error.NotFound(
        "CondicionTributaria.CondicionTributariaNoEncontrada",
        "No se pudo encontrar la condicion tributaria");
}
