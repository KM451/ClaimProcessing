using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ShipmentDate).IsRequired();
            builder.Property(p => p.PackingDetails).HasMaxLength(400);
            builder.Property(p => p.TotalWeight).HasPrecision(8, 2);
            builder.Property(p => p.SupplierId).IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.ModifiedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Modified);
            builder.Property(p => p.StatusId).IsRequired();
            builder.Property(p => p.InactivatedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Inactivated);
        }
    }
}
