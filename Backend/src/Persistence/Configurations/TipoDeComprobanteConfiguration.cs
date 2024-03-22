using Domain.Aggregates.VentaAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class TipoDeComprobanteConfiguration : IEntityTypeConfiguration<TipoDeComprobante>
{
    public void Configure(EntityTypeBuilder<TipoDeComprobante> builder)
    {
        builder.ToTable("TiposDeComprobante");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasColumnName("TipoDeComprobanteId");

        builder.HasMany(t => t.SeEmitePara).WithOne(sep => sep.TipoDeComprobante);
        builder.HasMany(t => t.EsEmitidoPor).WithOne(eep => eep.TipoDeComprobante);
    }
}
