using Application.DTOs.Ventas;

namespace Application.AppServices.Ventas.Queries.ObtenerVentas;

public sealed record ObtenerVentasResult(
    List<VentaDTO?> Ventas);
