using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class PackagingConfiguration : IEntityTypeConfiguration<Packaging>
    {
        public void Configure(EntityTypeBuilder<Packaging> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Type).HasMaxLength(40).IsRequired();
            builder.OwnsOne(p => p.Dimensions).Property(p => p.Height).HasColumnName("Height").IsRequired();
            builder.OwnsOne(p => p.Dimensions).Property(p => p.Width).HasColumnName("Width").IsRequired();
            builder.OwnsOne(p => p.Dimensions).Property(p => p.Depth).HasColumnName("Depth").IsRequired();
            builder.Property(p => p.Weight).HasPrecision(8, 2).IsRequired();
            builder.Property(p => p.Notes);
            builder.Property(p => p.ShipmentId).IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.ModifiedBy).HasMaxLength(40);
            builder.Property(p => p.InactivatedBy).HasMaxLength(40);
        }
    }
}
