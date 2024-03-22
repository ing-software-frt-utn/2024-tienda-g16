using Application.Abstractions.Messaging;
using Application.DTOs.Common;
using Application.Interfaces.Authentication;
using Domain.Abstractions;
using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.Aggregates.SesionAggregate;
using Domain.Aggregates.VendedorAggregate;
using Domain.Common.Errors;
using Domain.RepositoriesContracts;
using ErrorOr;

namespace Application.AppServices.Authentication.Commands.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPuntoDeVentaRepository _puntoDeVentaRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    public LoginCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPuntoDeVentaRepository puntoDeVentaRepository,
        ISesionRepository sesionRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _puntoDeVentaRepository = puntoDeVentaRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null || !_passwordHasher.Verify(user.Password, request.Password))
        {
            return UserErrors.CredencialesIncorrectas;
        }

        var puntoDeVenta = await _puntoDeVentaRepository.GetByIdAsync(new Guid(request.PuntoDeVentaId));
        if (puntoDeVenta is null)
        {
            return PuntoDeVentaErrors.PuntoDeVentaNoEncontrado;
        }

        var sesionUnica = await CrearSesionUnica(user.Vendedor, puntoDeVenta);
        if (sesionUnica.IsError) return sesionUnica.Errors;

        var sesion = sesionUnica.Value;
        var token = _jwtTokenGenerator.GenerateToken(sesion, user);

        return new LoginResult(
            User: UserDTO.ToDTO(user),
            Sesion: SesionDTO.ToDTO(sesion),
            Token: token);
    }

    private async Task<ErrorOr<Sesion>> CrearSesionUnica(Vendedor vendedor, PuntoDeVenta puntoDeVenta)
    {
        var vendedorSesion = await _sesionRepository.GetByVendedorIdAsync(vendedor.Id);
        var puntoDeVentaSesion = await _sesionRepository.GetByPuntoDeVentaIdAsync(puntoDeVenta.Id);

        bool puntoDeVentaLibre = puntoDeVentaSesion == null;
        bool vendedorLibre = vendedorSesion == null;
        bool sesionesIguales = vendedorSesion == puntoDeVentaSesion;

        bool vendedorEnSucursalCorrecta = vendedor.Sucursal == puntoDeVenta.Sucursal;
        bool refrescoDeToken = sesionesIguales && !vendedorLibre && !puntoDeVentaLibre;
        bool crearNuevaSesion = vendedorLibre && puntoDeVentaLibre;

        if (!vendedorEnSucursalCorrecta)
        {
            return SesionErrors.VendedorDeDistintaSucursal;
        }

        if (crearNuevaSesion)
        {
            var sesion = Sesion.Create(puntoDeVenta, vendedor);
            _sesionRepository.Add(sesion);
            await _unitOfWork.SaveChangesAsync();
            return sesion;
        }

        if (refrescoDeToken)
        {
            return vendedorSesion!;
        }

        return SesionErrors.MasDeUnaSesionActiva;
    }
}