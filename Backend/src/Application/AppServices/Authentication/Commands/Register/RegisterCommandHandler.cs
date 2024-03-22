using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Application.Interfaces.Authentication;
using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Authentication.Commands.Register;

public class RegisterCommandHandler
    : ICommandHandler<RegisterCommand, RegisterResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IVendedorRepository vendedorRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _vendedorRepository = vendedorRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var vendedor = await _vendedorRepository.GetByIdAsync(new Guid(request.VendedorId));
        if (vendedor is null)
        {
            return VendedorErrors.VendedorNoEncontrado;
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is not null)
        {
            return UserErrors.EmailEnUso;
        }

        var newUser = User.Create(
            vendedor,
            request.Email,
            _passwordHasher.Hash(request.Password));

        _userRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterResult(
            UserDTO.ToDTO(newUser),
            VendedorDTO.ToDTO(vendedor));
    }
}
