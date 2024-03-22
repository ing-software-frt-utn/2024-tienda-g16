using Domain.Aggregates.PuntoDeVentaAggregate;
using Domain.Aggregates.VendedorAggregate;
using Domain.Aggregates.VentaAggregate;

namespace Domain.RepositoriesContracts;

public interface IVentaRepository
{
    void Add(Venta venta);
    Task<Venta?> GetByIdAsync(Guid id);
    Task<Venta?> GetByIdWithAllAsync(Guid id);
    void Update(Venta venta);
    Task<Venta?> GetByVendedorAndPuntoDeVentaWithAllAsync(Vendedor vendedor, PuntoDeVenta puntoDeVenta);
    Task<Venta?> GetByVendedorAndPuntoDeVentaAsync(Vendedor vendedor, PuntoDeVenta puntoDeVenta);
    void Remove(Venta venta);
    Task<List<Venta>> GetAllAsync();
}
