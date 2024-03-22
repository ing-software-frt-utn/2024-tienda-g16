using ErrorOr;

namespace Domain.Common.Errors;

public static class VendedorErrors
{
    public static Error VendedorNoEncontrado => Error.NotFound(
        "Vendedor.NoEncontrado",
        "No se pudo encontrar al vendedor");
}
