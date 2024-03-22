using Application.Abstractions.Messaging;

namespace Application.AppServices.Ventas.Commands.AgregarPagoEfectivo;

public sealed record AgregarPagoEfectivoCommand(
    string VentaId,
    double Monto,
    string SesionId) : ICommand<VentaResult>;
