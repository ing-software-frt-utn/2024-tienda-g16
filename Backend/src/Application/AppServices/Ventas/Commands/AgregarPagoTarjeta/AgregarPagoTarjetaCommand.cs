using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.AgregarPagoTarjeta;

public sealed record AgregarPagoTarjetaCommand(
    string NumeroTarjeta,
    int MesExpiracion,
    int AnioExpiracion,
    int CodigoDeSeguridad,
    string NombreTitular,
    string ApellidoTitular,
    string Dni, 
    string SesionId, 
    string VentaId): ICommand<VentaResult>;