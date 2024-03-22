using Domain.Abstractions;
using Domain.Common.ValueObjects;
using Domain.Aggregates.VentaAggregate;
using Domain.Aggregates.VentaAggregate.Entities;

namespace Domain.Aggregates.ClienteAggregate;

public class Cliente : Entity
{
    private readonly List<Venta> _ventas = [];
    private Cliente(
        Guid id,
        NumeroDocumento numeroDocumento,
        string nombre,
        string apellido,
        CondicionTributaria condicionTributaria) : base(id)
    {
        NumeroDocumento = numeroDocumento;
        Nombre = nombre;
        Apellido = apellido;
        CondicionTributaria = condicionTributaria;
    }
    public NumeroDocumento NumeroDocumento { get; set; }
    public string Nombre { get; private set; }
    public string Apellido { get; private set; }
    public CondicionTributaria CondicionTributaria { get; private set; }
    public IReadOnlyList<Venta> Ventas { get => _ventas.ToList(); }

    public static Cliente Create(
        NumeroDocumento numeroDocumento,
        string nombre,
        string apellido,
        CondicionTributaria condicionTributaria)
    {
        return new Cliente(
            Guid.NewGuid(),
            numeroDocumento,
            nombre,
            apellido,
            condicionTributaria);
    }

    public static string GetDefaultNumeroDocumento()
    {
        return "0";
    }

    public bool IsClientePorDefecto()
    {
        return NumeroDocumento.Numero == GetDefaultNumeroDocumento();
    }

# pragma warning disable CS8618
    private Cliente(Guid id) : base(id) { }
# pragma warning restore CS8618
}
