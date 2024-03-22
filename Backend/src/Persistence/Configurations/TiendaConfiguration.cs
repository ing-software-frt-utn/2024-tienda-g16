using Domain.Aggregates.TiendaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class TiendaConfiguration : IEntityTypeConfiguration<Tienda>
{
    public void Configure(EntityTypeBuilder<Tienda> builder)
    {
        builder.ToTable("Tiendas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasColumnName("TiendaId");

        builder.HasMany(t => t.Sucursales).WithOne(su => su.Tienda);

        builder.HasOne(t => t.CondicionTributaria);
    }
}
