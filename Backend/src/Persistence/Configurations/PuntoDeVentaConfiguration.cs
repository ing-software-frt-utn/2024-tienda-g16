using Domain.Aggregates.PuntoDeVentaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PuntoDeVentaConfiguration : IEntityTypeConfiguration<PuntoDeVenta>
{
    public void Configure(EntityTypeBuilder<PuntoDeVenta> builder)
    {
        builder.ToTable("PuntosDeVenta");

        builder.HasOne(pdv => pdv.Sucursal);

        builder.HasKey(pdv => pdv.Id);

        builder.Property(pdv => pdv.Id)
            .ValueGeneratedNever()
            .HasColumnName("PuntoDeVentaId");
    }
}
