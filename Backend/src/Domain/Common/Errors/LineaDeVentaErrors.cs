using ErrorOr;

namespace Domain.Common.Errors;

public static class LineaDeVentaErrors
{
    public static Error CantidadNegativa => Error.Conflict(
        "LineaDeVenta.CantidadNegativa",
        "No se puede crear o agregar a una linea de venta con cantidad negativa");

    public static Error StockInsuficiente => Error.Conflict(
        "LineaDeVenta.StockInsuficiente",
        "La cantidad indicada es superior al stock disponible");
}
