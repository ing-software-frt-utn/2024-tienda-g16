using Domain.Aggregates.VendedorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.ToTable("Vendedores");

        builder.Property(v => v.Id)
            .ValueGeneratedNever()
            .HasColumnName("VendedorId");

        builder.HasMany(v => v.Users);
    }
}
