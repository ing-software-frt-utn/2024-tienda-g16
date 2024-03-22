using Domain.Aggregates.ClienteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasColumnName("ClienteId");

        builder.OwnsOne(c => c.NumeroDocumento, ndb =>
        {
            ndb.Property(nd => nd.Numero);
            ndb.Property(nd => nd.TipoDocumento);
            ndb.Property(nd => nd.Descripcion);

            ndb.WithOwner().HasForeignKey("ClienteId");
        });

        builder.HasOne(c => c.CondicionTributaria);
    }
}
