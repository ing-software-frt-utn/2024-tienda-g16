using Domain.Aggregates.VentaAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CondicionTributariaConfiguration : IEntityTypeConfiguration<CondicionTributaria>
{
    public void Configure(EntityTypeBuilder<CondicionTributaria> builder)
    {
        builder.ToTable("CondicionesTributarias");

        builder.HasKey(ctt => ctt.Id);

        builder.Property(ctt => ctt.Id)
            .ValueGeneratedNever()
            .HasColumnName("CondicionTributariaId");

        builder.Property(ctt => ctt.Descripcion);
    }
}
