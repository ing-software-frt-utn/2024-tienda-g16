using Domain.Abstractions;
using Domain.Common.Errors;
using ErrorOr;

namespace Domain.Common.DomainExceptions.Ventas;

public class VentaNoTieneLaLineaDeVentaDomainException : DomainException
{
    public VentaNoTieneLaLineaDeVentaDomainException() : base(VentaErrors.NoTieneLaLineaDeVenta)
    {
    }
}
