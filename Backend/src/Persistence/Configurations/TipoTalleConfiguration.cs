using Domain.Aggregates.StockAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class TipoTalleConfiguration : IEntityTypeConfiguration<TipoTalle>
{
    public void Configure(EntityTypeBuilder<TipoTalle> builder)
    {
        builder.ToTable("TiposTalle");

        builder.HasKey(tt => tt.Id);

        builder.Property(tt => tt.Id)
            .ValueGeneratedNever()
            .HasColumnName("TipoTalleId");
    }
}
