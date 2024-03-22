using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Clientes.Queries.ObtenerCliente;

public class ObtenerClientesQueryHandler : IQueryHandler<ObtenerClientesQuery, ObtenerClientesResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IClienteRepository _clienteRepository;

    public ObtenerClientesQueryHandler(
        ISesionRepository sesionRepository,
        IClienteRepository clienteRepository)
    {
        _sesionRepository = sesionRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<ErrorOr<ObtenerClientesResult>> Handle(ObtenerClientesQuery request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null) return SesionErrors.SesionInvalidada;

        var clientes = await _clienteRepository.GetAllAsync();

        var result = new ObtenerClientesResult(
            clientes.Select(c => ClienteDTO.ToDTO(c)).ToList());

        return result;
    }
}
