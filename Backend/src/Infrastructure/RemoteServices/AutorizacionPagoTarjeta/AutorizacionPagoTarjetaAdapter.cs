using Application.Adapters;
using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.Entities;
using Domain.Common.DomainExceptions.Gateways;
using ErrorOr;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace Infrastructure.RemoteServices.AutorizacionPagoTarjeta;

public class AutorizacionPagoTarjetaAdapter : IAutorizacionPagoTarjetaAdapter
{
    private readonly IHttpClientFactory _factory;
    private readonly DecidirSettings _decidirSettings;

    public AutorizacionPagoTarjetaAdapter(
        IHttpClientFactory factory,
        IOptions<DecidirSettings> decidirOptions)
    {
        _factory = factory;
        _decidirSettings = decidirOptions.Value;
    }

    public async Task AutorizarPagoTarjeta(
        Tarjeta tarjeta,
        Venta venta)
    {
        var httpClient = _factory.CreateClient(_decidirSettings.ClientName);

        var token = await ObtenerToken(httpClient, tarjeta, venta);
        if (token.IsError)
        {
            throw new AutorizarPagoTarjetaGatewayDomainException(token.FirstError);
        }

        var autorizacion = await ConfirmarPago(httpClient, venta, tarjeta, token.Value);
        if (autorizacion.IsError)
        {
            throw new AutorizarPagoTarjetaGatewayDomainException(autorizacion.FirstError);
        }
    }

    private async Task<ErrorOr<string>> ObtenerToken(HttpClient httpClient, Tarjeta tarjeta, Venta venta)
    {
        // Request
        var request = new HttpRequestMessage(HttpMethod.Post, "tokens");
        request.Headers.Add("apikey", _decidirSettings.TokenApiKey);

        // Content
        var getPaymentTokenRequest = new GetPaymentTokenRequest(
            tarjeta.NumeroTarjeta,
            tarjeta.MesExpiracion,
            tarjeta.AnioExpiracion,
            tarjeta.CodigoDeSeguridad.ToString(),
            tarjeta.NombreCompleto,
            new IdentificacionTitularRequest("Dni", tarjeta.DniTitular));

        var content = new StringContent(JsonSerializer.Serialize(getPaymentTokenRequest), null, "application/json");
        request.Content = content;

        // Response
        var response = await httpClient.SendAsync(request);

        ErrorOr<string> result = response.StatusCode switch
        {
            HttpStatusCode.Created => JsonSerializer.Deserialize<TokenReponse>(await response.Content.ReadAsStringAsync())!.id,
            _ => Error.Failure("No se pudo obtener el token")
        };

        return result;
    }
    private async Task<ErrorOr<Success>> ConfirmarPago(
        HttpClient httpClient,
        Venta venta,
        Tarjeta tarjeta,
        string token)
    {
        // Request
        var request = new HttpRequestMessage(HttpMethod.Post, "payments");
        request.Headers.Add("apikey", _decidirSettings.PaymentApiKey);

        // Content
        var confirmPaymentRequest = new ConfirmPaymentRequest(
            venta.Id,
            token,
            venta.Total,
            [new Subpayment(venta.Total)],
            tarjeta.NumeroTarjeta.Substring(0,6));

        var content = new StringContent(JsonSerializer.Serialize(confirmPaymentRequest), null, "application/json");
        request.Content = content;

        // Response
        var response = await httpClient.SendAsync(request);

        ErrorOr<Success> result = response.StatusCode switch
        {
            HttpStatusCode.Created => Result.Success,
            _ => Error.Failure("No se pudo confirmar el pago")
        };

        return result;
    }

    public sealed record TokenReponse(string id);
}
