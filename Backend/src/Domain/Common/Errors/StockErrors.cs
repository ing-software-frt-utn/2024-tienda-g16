using ErrorOr;

namespace Domain.Common.Errors;

public static class StockErrors
{
    public static Error StockNoEncontrado => Error.NotFound(
        "Stock.NoEncontrado",
        "No se pudo encontrar el stock");

    public static Error CantidadNegativa => Error.Conflict(
        "Stock.CantidadNegativa",
        "No se puede crear un stock con cantidad negativa");

    public static Error NoSePuedeReservar => Error.Conflict(
        "Stock.NoSePuedeReservar",
        "No se puede reservar mas que la cantidad disponible");

    public static Error NoSePuedeLiberar => Error.Conflict(
        "Stock.NoSePuedeLiberar",
        "No se puede liberar mas que la cantidad reservada");

    public static Error NoSePuedeConfirmar => Error.Failure(
        "Stock.NoSePuedeConfirmar",
        "Fatal: La cantidad reservada no es suficiente para confirmar la reduccion del stock");
}
