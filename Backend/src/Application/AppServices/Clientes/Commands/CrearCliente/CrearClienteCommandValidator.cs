using Application.Validation.Apellidos;
using Application.Validation.Ids;
using Application.Validation.Nombres;
using Application.Validation.NumerosDocumento;
using Domain.Enums;
using FluentValidation;

namespace Application.AppServices.Clientes.Commands.CrearCliente;

public class CrearClienteCommandValidator : AbstractValidator<CrearClienteCommand>
{
    public CrearClienteCommandValidator(
        DniValidator dniValidator,
        CuitValidator cuitValidator,
        NombreValidator nombreValidator,
        ApellidoValidator apellidoValidator,
        IdValidator idValidator)
    {
        RuleFor(x => x.Numero)
            .SetValidator(cuitValidator)
            .When(x => x.TipoDocumento == (int)TipoDocumento.Cuit);

        RuleFor(x => x.Numero)
            .SetValidator(dniValidator)
            .When(x => x.TipoDocumento == (int)TipoDocumento.Dni);

        RuleFor(x => x.TipoDocumento)
            .Must(t => Enum.IsDefined(typeof(TipoDocumento), t))
            .WithMessage($"El valor tipo de documento debe estar entre los valores {Enum.GetNames(typeof(TipoDocumento))}");

        RuleFor(x => x.TipoDocumento)
            .NotEqual((int)TipoDocumento.Anonimo)
            .WithMessage($"No se puede crear un cliente con el tipo de documento {TipoDocumento.Anonimo}");

        RuleFor(x => x.Nombre).SetValidator(nombreValidator);
        RuleFor(x => x.Apellido).SetValidator(apellidoValidator);
        RuleFor(x => x.CondicionTributariaId).SetValidator(idValidator);
        RuleFor(x => x.SesionId).SetValidator(idValidator);
    }
}
