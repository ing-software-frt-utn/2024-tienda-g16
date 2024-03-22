namespace Infrastructure.RemoteServices.AutorizacionPagoTarjeta;

public sealed record GetPaymentTokenRequest(
    string card_number,
    string card_expiration_month,
    string card_expiration_year,
    string security_code,
    string card_holder_name,
    IdentificacionTitularRequest card_holder_identification);

public sealed record IdentificacionTitularRequest(
    string type,
    string number);

