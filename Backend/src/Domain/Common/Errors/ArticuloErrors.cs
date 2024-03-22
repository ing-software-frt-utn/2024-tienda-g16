using ErrorOr;

namespace Domain.Common.Errors;

public static class ArticuloErrors
{
    public static Error CostoInvalido => Error.Validation(
        "Articulo.CostoInvalido",
        "El costo del articulo no puede ser ni nulo ni negativo");

    public static Error ArticuloNoEncontrado => Error.NotFound(
        "Articulo.NoEncontrado",
        "No se pudo encontrar el articulo");
}
