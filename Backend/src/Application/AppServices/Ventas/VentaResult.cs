using Application.DTOs.Ventas;

namespace Application.AppServices.Ventas;

public sealed record VentaResult(
    VentaDTO? Venta);
