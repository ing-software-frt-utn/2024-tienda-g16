using Application.AppServices.Ventas.Commands.AgregarLineaDeVenta;
using Application.AppServices.Ventas.Commands.AgregarPagoEfectivo;
using Application.AppServices.Ventas.Commands.AgregarPagoTarjeta;
using Application.AppServices.Ventas.Commands.AsociarCliente;
using Application.AppServices.Ventas.Commands.CancelarVenta;
using Application.AppServices.Ventas.Commands.ConfirmarVenta;
using Application.AppServices.Ventas.Commands.CrearNuevaVenta;
using Application.AppServices.Ventas.Commands.ModificarLineaDeVenta;
using Application.AppServices.Ventas.Queries.BuscarArticulo;
using Application.AppServices.Ventas.Queries.ObtenerVentaActual;
using Application.AppServices.Ventas.Queries.ObtenerVentas;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.AgregarLineaDeVenta;
using Presentation.Requests.AgregarPagoEfectivo;
using Presentation.Requests.AgregarPagoTarjeta;
using Presentation.Requests.AsociarCliente;
using Presentation.Requests.ModificarLineaDeVenta;
using Presentation.Responses.BuscarArticulo;
using Presentation.Responses.Ventas;
using System.Security.Claims;

namespace Presentation.Controllers.Ventas;

[ApiController]
[Route("venta")]
public class VentaController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public VentaController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CrearNuevaVenta()
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new CrearNuevaVentaCommand(
            sesionId);

        var result = await _mediator.Send(command);

        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<VentaResponse>(result.Value);
        return Ok(response);
    }

    [HttpGet]
    [Route("actual")]
    public async Task<IActionResult> ObtenerVentaActual()
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new ObtenerVentaActualQuery(
            sesionId);

        var result = await _mediator.Send(command);

        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<VentaResponse>(result.Value);
        return Ok(response);
    }

    [HttpGet]
    [Route("{codigoArticulo}")]
    public async Task<IActionResult> BuscarArticulo([FromRoute] string codigoArticulo)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;
        
        var query = new BuscarArticuloQuery(
            sesionId,
            codigoArticulo);

        var result = await _mediator.Send(query);
        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<BuscarArticuloResponse>(result.Value);
        return Ok(response);
    }

    [HttpPost]
    [Route("{ventaId}/lineaDeVenta")]
    public async Task<IActionResult> AgregarLineaDeVenta([FromRoute] string ventaId, AgregarLineaDeVentaRequest request)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new AgregarLineaDeVentaCommand(
            ventaId,
            request.CodigoArticulo,
            request.StockId,
            request.Cantidad,
            sesionId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);
        
        var response = _mapper.Map<VentaResponse>(result.Value);
        return Ok(response);
    }

    [HttpPatch]
    [Route("{ventaId}/lineaDeVenta")]
    public async Task<IActionResult> ModificarLineaDeVenta([FromRoute] string ventaId, ModificarLineaDeVentaRequest request)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new ModificarLineaDeVentaCommand(
            sesionId,
            ventaId,
            request.LineaDeVentaId, 
            request.Cantidad);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<VentaResponse>(result.Value);
        return Ok(response);
    }

    [HttpPatch]
    [Route("{ventaId}/pagoEfectivo")]
    public async Task<IActionResult> AgregarPagoEfectivo([FromRoute] string ventaId, AgregarPagoEfectivoRequest request)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new AgregarPagoEfectivoCommand(
            ventaId, 
            request.Monto, 
            sesionId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<VentaResponse>(result.Value);
        return Ok(response);
    }

    [HttpPatch]
    [Route("{ventaId}/pagoTarjeta")]
    public async Task<IActionResult> AgregarPagoTarjeta([FromRoute] string ventaId, AgregarPagoTarjetaRequest request)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new AgregarPagoTarjetaCommand(
            request.NumeroTarjeta,
            request.MesExpiracion,
            request.AnioExpiracion,
            request.CodigoDeSeguridad,
            request.NombreTitular,
            request.ApellidoTitular,
            request.DniTitular,
            sesionId,
            ventaId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        var response = _mapper.Map<VentaResponse>(result.Value);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{ventaId}")]
    public async Task<IActionResult> CancelarVenta([FromRoute] string ventaId)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new CancelarVentaCommand(
            sesionId,
            ventaId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return NoContent();
    }

    [HttpPatch]
    [Route("{ventaId}/cliente")]
    public async Task<IActionResult> AsociarCliente([FromRoute] string ventaId, AsociarClienteRequest request)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new AsociarClienteCommand(
            ventaId,
            request.ClienteId,
            sesionId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return Ok(result.Value);
    }

    [HttpPatch]
    [Route("{ventaId}/confirmar")]
    public async Task<IActionResult> ConfirmarVenta([FromRoute] string ventaId)
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var command = new ConfirmarVentaCommand(
            ventaId,
            sesionId);

        var result = await _mediator.Send(command);
        if (result.IsError) return Problem(result.Errors);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerVentas()
    {
        var sesionId = HttpContext.User.FindFirstValue("SesionId")!;

        var query = new ObtenerVentasQuery(
            sesionId);

        var result = await _mediator.Send(query);
        if (result.IsError) return Problem(result.Errors);

        return Ok(result.Value);
    }
}
