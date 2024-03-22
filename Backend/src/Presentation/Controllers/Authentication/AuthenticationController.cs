using Application.AppServices.Authentication.Commands.Login;
using Application.AppServices.Authentication.Commands.Logout;
using Application.AppServices.Authentication.Commands.Register;
using Application.AppServices.Authentication.Queries.Sesiones;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.Authentication;
using Presentation.Responses.GetSesionesActivas;
using Presentation.Responses.Login;
using Presentation.Responses.Register;

namespace Presentation.Controllers.Authentication;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginCommand(
            request.Email,
            request.Password,
            request.PuntoDeVentaId);

        var result = await _mediator.Send(query);

        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<LoginResponse>(result.Value);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.Email,
            request.Password,
            request.VendedorId);

        var result = await _mediator.Send(command);

        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<RegisterResponse>(result.Value);
        return Ok(response);
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var sesionId = User.FindFirst("SesionId")!.Value;

        var command = new LogoutCommand(sesionId);

        var result = await _mediator.Send(command);

        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<LogoutResponse>(result.Value);
        return Ok(response);
    }

    [HttpGet("sesion")]
    [AllowAnonymous]
    public async Task<IActionResult> GetActiveSessions()
    {
        var query = new GetSesionesActivasQuery();
        var result = await _mediator.Send(query);

        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<GetSesionesActivasResponse>(result.Value);
        return Ok(response);
    }
}
