using Domain.Aggregates.VentaAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SeEmiteParaConfiguration : IEntityTypeConfiguration<SeEmitePara>
{
    public void Configure(EntityTypeBuilder<SeEmitePara> builder)
    {
        builder.ToTable("SeEmitePara");

        builder.HasKey(sep => sep.Id);

        builder.Property(cc => cc.Id)
            .ValueGeneratedNever()
            .HasColumnName("SeEmiteParaId");

        builder.HasOne(sep => sep.CondicionTributaria);
        builder.HasOne(sep => sep.TipoDeComprobante);
    }
}
