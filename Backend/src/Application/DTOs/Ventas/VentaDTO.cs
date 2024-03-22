using Application.DTOs.Common;
using Domain.Aggregates.VentaAggregate;

namespace Application.DTOs.Ventas;

public sealed record VentaDTO
{
    public Guid Id { get; private set; }
    public DateTime? CreadaUtc { get; private set; }
    public bool Confirmada { get; private set; }
    public DateTime? ConfirmadaUtc { get; private set; }
    public CodigoComprobanteDTO? CodigoComprobante { get; private set; }
    public VendedorDTO? Vendedor { get; private set; }
    public PuntoDeVentaDTO? PuntoDeVenta { get; private set; }
    public ClienteDTO? Cliente { get; private set; }
    public TipoDeComprobanteDTO? TipoDeComprobante { get; private set; }
    public List<LineaDeVentaDTO?> LineasDeVenta { get; private set; } = [];
    public PagoDTO? Pago { get; private set; }
    public double Total { get; private set; }

    public static VentaDTO? ToDTO(Venta? venta)
    {
        if (venta is null)
        {
            return null;
        }

        return new VentaDTO
        {
            Id = venta.Id,
            CreadaUtc = venta.CreadaUtc,
            Confirmada = venta.Confirmada,
            ConfirmadaUtc = venta.ConfirmadaUtc,
            CodigoComprobante = CodigoComprobanteDTO.ToDTO(venta.CodigoComprobante),
            Vendedor = VendedorDTO.ToDTO(venta.Vendedor),
            Cliente = ClienteDTO.ToDTO(venta.Cliente),
            TipoDeComprobante = TipoDeComprobanteDTO.ToDTO(venta.TipoDeComprobante),
            LineasDeVenta = venta.LineasDeVenta
                .Select(lv => LineaDeVentaDTO.ToDTO(lv))
                .Where(lvDTO => lvDTO is not null)
                .ToList(),
            Pago = PagoDTO.ToDTO(venta.Pago),
            Total = venta.Total,
            PuntoDeVenta = PuntoDeVentaDTO.ToDTO(venta.PuntoDeVenta),
        };
    }
}
