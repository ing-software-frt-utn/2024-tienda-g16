using Application.AppServices.CondicionesTributarias.Queries.ObtenerCondicionesTributarias;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers.CondicionesTributarias;

[ApiController]
[Route("condicionTributaria")]
public class CondicionTributariaController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public CondicionTributariaController(
        ISender mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerCondicionesTributarias()
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var query = new ObtenerCondicionesTributariasQuery(
            sesionId);

        var result = await _mediator.Send(query);
        if (result.IsError) return Problem(result.Errors);

        return Ok(result.Value);
    }
}
