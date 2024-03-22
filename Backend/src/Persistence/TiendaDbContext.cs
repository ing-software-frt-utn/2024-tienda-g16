using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.VendedorAggregate;
using Domain.Aggregates.VentaAggregate;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class TiendaDbContext : DbContext
{
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public TiendaDbContext(DbContextOptions<TiendaDbContext> options) 
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TiendaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
