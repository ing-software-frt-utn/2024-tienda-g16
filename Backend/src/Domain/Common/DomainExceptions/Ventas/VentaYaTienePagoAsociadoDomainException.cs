using Domain.Abstractions;
using Domain.Common.Errors;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaYaTienePagoAsociadoDomainException : DomainException
{
    public VentaYaTienePagoAsociadoDomainException() : base(VentaErrors.YaTieneAsociadoUnPago)
    {
    }
}
