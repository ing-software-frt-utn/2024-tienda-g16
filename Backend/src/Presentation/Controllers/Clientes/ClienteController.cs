using Application.AppServices.Clientes.Commands.CrearCliente;
using Application.AppServices.Clientes.Queries.ObtenerCliente;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.CrearCliente;
using System.Security.Claims;

namespace Presentation.Controllers.Clientes;

[ApiController]
[Route("cliente")]
public class ClienteController : ApiController
{
    private readonly ISender _mediator;

    public ClienteController(
        ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerClientes()
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var query = new ObtenerClientesQuery(
            sesionId);

        var result = await _mediator.Send(query);
        if (result.IsError) return Problem(result.Errors);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CrearCliente(CrearClienteRequest request)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new CrearClienteCommand(
            request.Nombre,
            request.Apellido,
            request.Numero,
            request.TipoDocumento,
            request.CondicionTributariaId,
            sesionId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return Ok(result.Value);
    }
}
