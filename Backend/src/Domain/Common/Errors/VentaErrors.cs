using ErrorOr;

namespace Domain.Common.Errors;

public static class VentaErrors
{
    public static Error NoSePuedeModificarConfirmada => Error.Conflict(
        "Venta.VentaConfirmada",
        "No es posible modificar una venta que ya fue confirmada");

    public static Error AsociarElMismoCliente => Error.Conflict(
        "Venta.AsociarCliente",
        "No se puede asociar el mismo cliente que ya tiene asociado");

    public static Error YaTieneAsociadoUnPago => Error.Conflict(
        "Venta.PagoAsociado",
        "La venta ya tiene un pago asociado");

    public static Error StockDeOtraSucursal => Error.Conflict(
        "Venta.StockDeOtraSucursal",
        "No se puede agregar a la venta un stock de otra sucursal");

    public static Error VentaNoEncontrada => Error.NotFound(
        "Venta.NoEncontrada",
        "No se pudo encontrar la venta");

    public static Error EsDeOtroVendedor => Error.Conflict(
        "Venta.EsDeOtroVendedor",
        "La venta fue creada por otro vendedor");

    public static Error EsDeOtroPuntoDeVenta => Error.Conflict(
        "Venta.EsDeOtroPuntoDeVenta",
        "La venta fue creada en otro punto de venta");

    public static Error NoTieneLaLineaDeVenta => Error.NotFound(
        "Venta.NoTieneLineaDeVenta",
        "La venta no tiene la linea de venta indicada");

    public static Error YaHayUnaVentaEnProceso => Error.Conflict(
        "Venta.YaHayUnaVentaEnProceso",
        "Ya hay una venta en proceso para esta sesion");

    public static Error NoSePuedePagarVentaVacia => Error.Conflict(
        "Venta.NoSePuedePagarVentaVacia",
        "No es posible pagar una venta vacia");

    public static Error VentaYaConfirmada => Error.Conflict(
        "Venta.VentaYaConfirmada",
        "La venta ya fue confirmada");

    public static Error VentaNoPagada => Error.Conflict(
       "Venta.VentaNoPagada",
       "La venta no se puede confirmar porque no fue pagada");

    public static Error NoSePuedeCancelarPagada => Error.Conflict(
        "Venta.VentaPagada",
        "No se puede cancelar una venta pagada");

    public static Error LaVentaNoPuedeSerAnonima => Error.Conflict(
        "Venta.NoPuedeSerAnonima",
        "El monto de la venta supera el maximo establecido por la AFIP, por lo tanto no puede ser anonima");

    public static Error MontoInsuficiente => Error.Conflict(
        "Venta.MontoInsuficiente",
        "El monto abonado no es suficiente");
}
