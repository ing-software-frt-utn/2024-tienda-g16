using Application.Validation.AniosExpiracion;
using Application.Validation.Apellidos;
using Application.Validation.Ids;
using Application.Validation.MesesExpiracion;
using Application.Validation.Nombres;
using Application.Validation.NumerosDocumento;
using FluentValidation;

namespace Application.AppServices.Ventas.Commands.AgregarPagoTarjeta;

public class AgregarPagoTarjetaCommandValidator : AbstractValidator<AgregarPagoTarjetaCommand>
{
    public AgregarPagoTarjetaCommandValidator(
        MesExpiracionValidator mesExpiracionValidator,
        AnioExpiracionValidator anioExpiracionValidator,
        NombreValidator nombreValidator,
        ApellidoValidator apellidoValidator, 
        DniValidator dniValidator)
    {
        RuleFor(x => x.NumeroTarjeta).CreditCard();
        RuleFor(x => x.MesExpiracion).SetValidator(mesExpiracionValidator);
        RuleFor(x => x.AnioExpiracion).SetValidator(anioExpiracionValidator);
        RuleFor(x => x.NombreTitular).SetValidator(nombreValidator);
        RuleFor(x => x.ApellidoTitular).SetValidator(apellidoValidator);
        RuleFor(x => x.Dni).SetValidator(dniValidator);

        RuleFor(x => x.CodigoDeSeguridad)
            .NotEmpty()
            .WithMessage("El codigo de seguridad no puede ser nulo");
    }
}
