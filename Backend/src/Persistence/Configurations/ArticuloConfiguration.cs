using Domain.Aggregates.ArticuloAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ArticuloConfiguration : IEntityTypeConfiguration<Articulo>
{
    public void Configure(EntityTypeBuilder<Articulo> builder)
    {
        builder.ToTable("Articulos");

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasColumnName("ArticuloId");

        builder.HasMany(a => a.Stocks).WithOne(s => s.Articulo);
    }
}
