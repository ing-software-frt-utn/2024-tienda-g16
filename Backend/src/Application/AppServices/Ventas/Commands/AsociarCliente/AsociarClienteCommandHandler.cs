using Application.Abstractions.Messaging;
using Application.DTOs.Ventas;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Ventas.Commands.AsociarCliente;

public class AsociarClienteCommandHandler : ICommandHandler<AsociarClienteCommand, VentaResult>
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AsociarClienteCommandHandler(
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
    public async Task<ErrorOr<VentaResult>> Handle(AsociarClienteCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null) return SesionErrors.SesionInvalidada;

        var venta = await _ventaRepository.GetByIdWithAllAsync(new Guid(request.VentaId));
        if (venta == null) return VentaErrors.VentaNoEncontrada;

        var cliente = await _clienteRepository.GetByIdAsync(new Guid(request.ClienteId));
        if (cliente == null) return ClienteErrors.ClienteNoEncontrado;

        try
        {
            venta.AsociarCliente(cliente);
        } 
        catch (DomainException exc)
        {
            return exc.Error;
        }

        await _unitOfWork.SaveChangesAsync();

        return new VentaResult(VentaDTO.ToDTO(venta));
    }
}
