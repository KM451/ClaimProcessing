using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.OwnerType).HasMaxLength(40).IsRequired();
            builder.Property(p => p.ClaimType).HasMaxLength(40).IsRequired();
            builder.Property(p => p.ItemCode).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Qty).HasPrecision(10,2).IsRequired();
            builder.Property(p => p.CustomerName).HasMaxLength(100);
            builder.Property(p => p.ItemName).HasMaxLength(100);
            builder.Property(p => p.ClaimDescription);
            builder.Property(p => p.Remarks);
            builder.Property(p => p.ClaimStatus).HasMaxLength(40).IsRequired();
            builder.Property(p => p.RmaAvailable).HasDefaultValue(false).IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(40);
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.ModifiedBy).HasMaxLength(40);
            builder.Property(p => p.InactivatedBy).HasMaxLength(40);

        }
    }
}
