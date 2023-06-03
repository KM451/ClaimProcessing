using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class PurchaseDetailConfiguration : IEntityTypeConfiguration<PurchaseDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseInvoiceNo).HasMaxLength(40).IsRequired();
            builder.Property(p => p.PurchaseDate).IsRequired();
            builder.Property(p => p.InternalDocNo).HasMaxLength(40);
            builder.Property(p => p.ClaimId).IsRequired();
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
