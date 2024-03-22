using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Domain.Abstractions;
using Domain.Aggregates.ClienteAggregate;
using Domain.Common.Errors;
using Domain.Common.ValueObjects;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Clientes.Commands.CrearCliente;

public class CrearClienteCommandHandler : ICommandHandler<CrearClienteCommand, CrearClienteResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly ICondicionTributariaRepository _condicionTributariaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearClienteCommandHandler(
        ISesionRepository sesionRepository,
        ICondicionTributariaRepository condicionTributariaRepository,
        IUnitOfWork unitOfWork,
        IClienteRepository clienteRepository)
    {
        _sesionRepository = sesionRepository;
        _condicionTributariaRepository = condicionTributariaRepository;
        _unitOfWork = unitOfWork;
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<CrearClienteResult>> Handle(CrearClienteCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null) return SesionErrors.SesionInvalidada;

        var condicionTributaria = await _condicionTributariaRepository.GetByIdAsync(new Guid(request.CondicionTributariaId));
        if (condicionTributaria == null) return CondicionTributariaErrors.CondicionTributariaNoEncontrada;

        var numeroDocumento = NumeroDocumento.Create(
            request.TipoDocumento,
            request.Numero);

        var cliente = Cliente.Create(
            numeroDocumento,
            request.Nombre,
            request.Apellido,
            condicionTributaria);

        _clienteRepository.Add(cliente);

        await _unitOfWork.SaveChangesAsync();

        return new CrearClienteResult(ClienteDTO.ToDTO(cliente));
    }
}
