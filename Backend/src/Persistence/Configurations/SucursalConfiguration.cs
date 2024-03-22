using Domain.Aggregates.SucursalAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SucursalConfiguration : IEntityTypeConfiguration<Sucursal>
{
    public void Configure(EntityTypeBuilder<Sucursal> builder)
    {
        builder.ToTable("Sucursales");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasColumnName("SucursalId");

        builder.HasMany(su => su.PuntosDeVenta).WithOne(pdv => pdv.Sucursal);

        builder.HasMany(su => su.Vendedores).WithOne(v => v.Sucursal);
    }
}
