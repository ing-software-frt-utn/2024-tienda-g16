using Domain.Aggregates.SesionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SesionConfiguration : IEntityTypeConfiguration<Sesion>
{
    public void Configure(EntityTypeBuilder<Sesion> builder)
    {
        builder.ToTable("Sesiones");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasColumnName("SesionId");

        builder.HasOne(s => s.Vendedor);
        builder.HasOne(s => s.PuntoDeVenta);
    }
}
