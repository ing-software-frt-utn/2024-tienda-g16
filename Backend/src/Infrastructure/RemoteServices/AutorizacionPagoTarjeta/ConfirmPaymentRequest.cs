namespace Infrastructure.RemoteServices.AutorizacionPagoTarjeta;

public sealed record ConfirmPaymentRequest(
    Guid site_transaction_id,
    string token,
    double amount,
    List<Subpayment> sub_payments,
    string bin,
    int payment_method_id = 1,
    string currency = "ARS",
    int installments = 1,
    string payment_type = "single",
    string establishment_name = "single");

public sealed record Subpayment(
    double amount,
    string site_id = "",
    string? installments = null);
