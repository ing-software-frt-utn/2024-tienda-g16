using ErrorOr;

namespace Domain.Common.Errors;

public static class PuntoDeVentaErrors
{
    public static Error PuntoDeVentaNoEncontrado => Error.NotFound(
        "PuntoDeVenta.NoEncontrado",
        "No se pudo encontrar el punto de venta");
}
