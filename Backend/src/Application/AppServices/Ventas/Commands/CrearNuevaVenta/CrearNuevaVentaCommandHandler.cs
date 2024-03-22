using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Abstractions;
using Domain.Aggregates.ClienteAggregate;
using Domain.Aggregates.VentaAggregate;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.CrearNuevaVenta;

public class CrearNuevaVentaCommandHandler : ICommandHandler<CrearNuevaVentaCommand, VentaResult>
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearNuevaVentaCommandHandler(
        IVentaRepository ventaRepository,
        IUnitOfWork unitOfWork,
        IClienteRepository clienteRepository,
        ISesionRepository sesionRepository)
    {
        _ventaRepository = ventaRepository;
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
        _sesionRepository = sesionRepository;
    }

    public async Task<ErrorOr<VentaResult>> Handle(CrearNuevaVentaCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion is null) return SesionErrors.SesionInvalidada;

        var clientePorDefecto = await _clienteRepository.GetByNumeroDocumentoAsync(Cliente.GetDefaultNumeroDocumento());
        if (clientePorDefecto is null) return ClienteErrors.ClientePorDefectoNoEncontrado;

        var ventaActual = await _ventaRepository.GetByVendedorAndPuntoDeVentaAsync(sesion.Vendedor, sesion.PuntoDeVenta);
        if (ventaActual != null) return VentaErrors.YaHayUnaVentaEnProceso;

        var venta = Venta.Create(sesion.PuntoDeVenta, sesion.Vendedor);

        try
        {
            venta.AsociarCliente(clientePorDefecto);
        }
        catch (DomainException exc)
        {
            return exc.Error;
        }

        _ventaRepository.Add(venta);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new VentaResult(VentaDTO.ToDTO(venta));
    }
}
