using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class FotoUrlConfiguration : IEntityTypeConfiguration<FotoUrl>
    {
        public void Configure(EntityTypeBuilder<FotoUrl> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Path).HasMaxLength(200).IsRequired();
            builder.Property(p => p.ClaimId).IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.ModifiedBy).HasMaxLength(40);
            builder.Property(p => p.InactivatedBy).HasMaxLength(40);
        }
    }
}
