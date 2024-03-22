using Domain.Aggregates.StockAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("StockId")
            .ValueGeneratedNever();

        builder.HasOne(s => s.Articulo).WithMany(s => s.Stocks);
        builder.HasOne(s => s.Talle);
        builder.HasOne(s => s.Color);
    }
}
