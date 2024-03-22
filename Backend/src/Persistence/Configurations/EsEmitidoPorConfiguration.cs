using Domain.Aggregates.VentaAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class EsEmitidoPorConfiguration : IEntityTypeConfiguration<EsEmitidoPor>
{
    public void Configure(EntityTypeBuilder<EsEmitidoPor> builder)
    {
        builder.ToTable("EsEmitidoPor");

        builder.HasKey(eep => eep.Id);

        builder.Property(cc => cc.Id)
            .ValueGeneratedNever()
            .HasColumnName("EsEmitidoPorId");

        builder.HasOne(eep => eep.CondicionTributaria);
        builder.HasOne(eep => eep.TipoDeComprobante);
    }
}
