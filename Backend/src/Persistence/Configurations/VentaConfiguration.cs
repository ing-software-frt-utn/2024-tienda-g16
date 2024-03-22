using Domain.Aggregates.VentaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Ventas");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedNever()
                .HasColumnName("VentaId");

            builder.HasOne(v => v.Vendedor).WithMany(v => v.Ventas);

            builder.HasOne(v => v.PuntoDeVenta).WithMany(pv => pv.Ventas);

            builder.HasOne(v => v.TipoDeComprobante);

            builder.OwnsOne(v => v.Pago, pb =>
            {
                pb.ToTable("Pagos");

                pb.HasKey(pb => pb.Id);

                pb.Property(pb => pb.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("PagoId");

                pb.WithOwner().HasForeignKey("VentaId");
            });

            builder.OwnsOne(v => v.CodigoComprobante, ccb =>
            {
                ccb.Property(cc => cc.Numero);
                ccb.Property(cc => cc.FechaVencimiento);
            });

            builder.HasOne(v => v.Cliente).WithMany(c => c.Ventas);

            builder.OwnsMany(v => v.LineasDeVenta, lvb =>
            {
                lvb.ToTable("LineasDeVenta");

                lvb.HasKey(lv => lv.Id);

                lvb.Property(lv => lv.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("LineaDeVentaId");

                lvb.HasOne(lv => lv.Stock);

                lvb.WithOwner().HasForeignKey("VentaId");
            });

            
        }
    }
}
