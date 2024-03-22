using Domain.Aggregates.StockAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class TalleConfiguration : IEntityTypeConfiguration<Talle>
{
    public void Configure(EntityTypeBuilder<Talle> builder)
    {
        builder.ToTable("Talles");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .HasColumnName("TalleId");

        builder.HasOne(t => t.TipoTalle);
    }
}
