namespace Infrastructure.RemoteServices.AutorizacionPagoTarjeta;

public class DecidirSettings
{
    public const string SectionName = "DecidirSettings";
    public string ClientName { get; init; } = string.Empty;
    public string TokenApiKey { get; init; } = string.Empty;
    public string PaymentApiKey { get; init; } = string.Empty;
}
