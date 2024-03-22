namespace Domain.Aggregates.VentaAggregate.Entities;

public class Tarjeta
{
    public Tarjeta(
        string numeroTarjeta,
        int mesExpiracion,
        int anioExpiracion,
        int codigoDeSeguridad,
        string nombreTitular,
        string apellidoTitular, 
        string dniTitular)
    {
        NumeroTarjeta = numeroTarjeta;
        MesExpiracion = mesExpiracion.ToString().PadLeft(2, '0');
        AnioExpiracion = anioExpiracion.ToString().PadLeft(2, '0');
        CodigoDeSeguridad = codigoDeSeguridad;
        NombreTitular = nombreTitular;
        ApellidoTitular = apellidoTitular;
        DniTitular = dniTitular;
    }
    public string NumeroTarjeta { get; private set; }
    public string MesExpiracion { get; private set; }
    public string AnioExpiracion { get; private set; }
    public int CodigoDeSeguridad { get; private set; }
    public string NombreTitular { get; private set; }
    public string ApellidoTitular { get; private set; }
    public string DniTitular { get; private set; }
    public string NombreCompleto => $"{ApellidoTitular} {NombreTitular}";
}
