namespace Presentation.Requests.AgregarPagoTarjeta;

public sealed record AgregarPagoTarjetaRequest(
    string NumeroTarjeta, 
    int MesExpiracion,
    int AnioExpiracion, 
    int CodigoDeSeguridad,
    string NombreTitular,
    string ApellidoTitular,
    string DniTitular);
