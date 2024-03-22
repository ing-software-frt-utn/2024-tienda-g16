using Application.Adapters;
using AutorizacionAfipService;
using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.ValueObjects;
using Domain.Common.DomainExceptions.Gateways;
using Domain.Primitives;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Infrastructure.RemoteServices.AutorizacionAfipService;

public class AutorizacionAfipAdapter : IAutorizacionAfipAdapter
{
    private readonly ILoginService _loginService;
    private static Dictionary<ComprobanteTipo, int> _numeracionComprobantes = [];
    private readonly AfipSettings _afipSettings;
    private readonly int _defaultValue = -1;

    public AutorizacionAfipAdapter(
        ILoginService loginService,
        IOptions<AfipSettings> afipOptions)
    {
        _loginService = loginService;
        _afipSettings = afipOptions.Value;
    }

    public async Task<CodigoComprobante> AutorizarVenta(Venta venta)
    {
        var token = await SolicitarAutorizacion(_afipSettings.GroupCode);
        if (token.IsError)
        {
            throw new AutorizacionAfipGatewayDomainException(token.FirstError);
        }

        var ultimoNumeroComprobante = _numeracionComprobantes.GetValueOrDefault(venta.TipoDeComprobante!.ComprobanteTipo, _defaultValue);
        if (ultimoNumeroComprobante == _defaultValue)
        {
            var solicitarComprobantes = await SolicitarUltimosComprobantes(token.Value);
            if (solicitarComprobantes.IsError)
            {
                throw new AutorizacionAfipGatewayDomainException(solicitarComprobantes.FirstError);
            }
        }

        var comprobanteTipo = venta.TipoDeComprobante!.ComprobanteTipo;
        ultimoNumeroComprobante = _numeracionComprobantes.GetValueOrDefault(comprobanteTipo, _defaultValue);

        var codigoComprobante = await SolicitarCae(venta, ultimoNumeroComprobante, token.Value);
        if (codigoComprobante.IsError)
        {
            throw new AutorizacionAfipGatewayDomainException(codigoComprobante.FirstError);
        }

        _numeracionComprobantes[comprobanteTipo] += 1;

        return codigoComprobante.Value;
    }

    public async Task<ErrorOr<string>> SolicitarAutorizacion(string codigo)
    {
        var autorizacion = await _loginService.SolicitarAutorizacionAsync(codigo);

        if (autorizacion.Error is not null)
        {
            return Error.Failure("Error al solicitar token", autorizacion.Error);
        }

        return autorizacion.Token;
    }

    public async Task<ErrorOr<Success>> SolicitarUltimosComprobantes(string token)
    {
        var ultimoComprobante = await _loginService.SolicitarUltimosComprobantesAsync(token);

        if (ultimoComprobante.Error  != null)
        {
            return Error.Failure("Error al tratar de obtener numeracion de comprobantes", ultimoComprobante.Error);
        }

        var dict = new Dictionary<ComprobanteTipo, int>();

        foreach(Comprobante comprobante in ultimoComprobante.Comprobantes)
        {
            ErrorOr<ComprobanteTipo> comprobanteTipo = comprobante.Id switch
            {
                1 => ComprobanteTipo.FacturaA,
                6 => ComprobanteTipo.FacturaB,
                _ => Error.Failure("El sistema no conoce todos los tipos de comprobantes", $"No se pudo convertir el tipo {comprobante.Id}"),
            };

            if (comprobanteTipo.IsError)
            {
                return comprobanteTipo.FirstError;
            }

            dict.Add(comprobanteTipo.Value, comprobante.Numero);
        }

        _numeracionComprobantes = dict;
        return Result.Success;
    }

    public async Task<ErrorOr<CodigoComprobante>> SolicitarCae(Venta venta, int numero, string token)
    {
        var tipoComprobante = MapToTipoComprobante(venta.TipoDeComprobante!.ComprobanteTipo);
        var tipoDocumento = MapToTipoDocumento(venta.Cliente!.NumeroDocumento.TipoDocumento);
        long numeroDocumento = long.Parse(venta.Cliente!.NumeroDocumento.Numero);
        var isDocumentoCorrectForFactura = ValidarDocumentoParaTipoFactura(tipoComprobante, tipoDocumento);

        if (!isDocumentoCorrectForFactura)
        {
            return Error.Conflict("Error en el tipo de documento usado",
                $"El tipo de documento {tipoDocumento} no es valido para el comprobante {tipoComprobante}");
        }

        var solicitud = new SolicitudAutorizacion() {
            Fecha = DateTime.UtcNow,
            ImporteIva = venta.ImporteIva,
            ImporteNeto = venta.ImporteNeto,
            ImporteTotal = venta.Total,
            NumeroDocumento = numeroDocumento,
            TipoComprobante = tipoComprobante,
            TipoDocumento = tipoDocumento,
            Numero = numero + 1,
        };

        var resultadoCae = await _loginService.SolicitarCaeAsync(token, solicitud);

        if (resultadoCae.Error != null)
        {
            return Error.Failure("No se pudo obtener el Cae", resultadoCae.Error);
        }

        if (resultadoCae.Estado != EstadoSolicitud.Aprobada)
        {
            return Error.Conflict("La solicitud fue rechazada", resultadoCae.Observacion);
        }

        return CodigoComprobante.Create(
            resultadoCae.Cae,
            DateTime.ParseExact(resultadoCae.FechaDeVencimiento, "yyyyMMdd", CultureInfo.InvariantCulture));
    }

    private bool ValidarDocumentoParaTipoFactura(TipoComprobante tipoComprobante, TipoDocumento tipoDocumento)
    {
        if(tipoComprobante == TipoComprobante.FacturaA)
        {
            return tipoDocumento == TipoDocumento.Cuit;
        }

        if(tipoComprobante == TipoComprobante.FacturaB)
        {
            return tipoDocumento == TipoDocumento.Dni || 
                tipoDocumento == TipoDocumento.ConsumidorFinal;
        }

        return false;
    }

    private TipoComprobante MapToTipoComprobante(ComprobanteTipo comprobanteTipo)
    {
        switch (comprobanteTipo)
        {
            case ComprobanteTipo.FacturaA:
                return TipoComprobante.FacturaA;
            case ComprobanteTipo.FacturaB:
                return TipoComprobante.FacturaB;
            default:
                return TipoComprobante.FacturaB;
        };
    }

    private TipoDocumento MapToTipoDocumento(Domain.Enums.TipoDocumento tipoDocumento)
    {
        return tipoDocumento switch
        {
            Domain.Enums.TipoDocumento.Anonimo => TipoDocumento.ConsumidorFinal,
            Domain.Enums.TipoDocumento.Cuit => TipoDocumento.Cuit,
            Domain.Enums.TipoDocumento.Dni => TipoDocumento.Dni,
        };
    }
}
