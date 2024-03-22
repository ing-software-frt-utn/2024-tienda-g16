using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Application.Interfaces.Authentication;
using Domain.Abstractions;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Authentication.Commands.Logout;

public class LogoutCommandHandler : ICommandHandler<LogoutCommand, LogoutResult>
{
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWork _unitOfWork;
    public LogoutCommandHandler(
        ISesionRepository sesionRepository,
        IUnitOfWork unitOfWork)
    {
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<LogoutResult>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var sesion = await _sesionRepository.GetByIdAsync(new Guid(request.SesionId));
        if (sesion == null) return SesionErrors.SesionNoEncontrada;
        
        _sesionRepository.Remove(sesion);
        await _unitOfWork.SaveChangesAsync();

        var sesiones = await _sesionRepository.GetAllAsync();
        return new LogoutResult(
            VendedorDTO.ToDTO(sesion.Vendedor),
            sesiones.Select(s => SesionDTO.ToDTO(s)).ToList());
    }
}
