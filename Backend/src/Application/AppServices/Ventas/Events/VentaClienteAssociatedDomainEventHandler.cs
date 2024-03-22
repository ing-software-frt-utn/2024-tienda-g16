using Domain.Aggregates.TiendaAggregate;
using Domain.DomainEvents;
using Domain.RepositoriesContracts;
using MediatR;

namespace Application.AppServices.Ventas.Events;

public sealed class VentaClienteAssociatedDomainEventHandler : INotificationHandler<VentaClienteAssociatedDomainEvent>
{
    private readonly ITipoDeComprobanteRepository _tipoDeComprobanteRepository;

    public VentaClienteAssociatedDomainEventHandler(ITipoDeComprobanteRepository tipoDeComprobanteRepository)
    {
        _tipoDeComprobanteRepository = tipoDeComprobanteRepository;
    }

    public async Task Handle(VentaClienteAssociatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var venta = notification.Venta;
        var cttCliente = notification.Cliente.CondicionTributaria;
        var cttTienda = Tienda.Instance.GetTienda().CondicionTributaria;

        var tipoDeComprobante = await _tipoDeComprobanteRepository
            .GetByCondicionesTributarias(seEmitePara: cttCliente, esEmitidoPor: cttTienda);

        venta.AsociarTipoComprobante(tipoDeComprobante!);
    }
}
