using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(80).IsRequired();
            builder.OwnsOne(s => s.Address).Property(p=>p.Street).HasColumnName("Street").IsRequired();
            builder.OwnsOne(s => s.Address).Property(p=>p.City).HasColumnName("City").IsRequired();
            builder.OwnsOne(s => s.Address).Property(p=>p.ZipCode).HasColumnName("ZipCode").IsRequired();
            builder.OwnsOne(s => s.Address).Property(p=>p.Country).HasColumnName("Country").IsRequired();
            builder.OwnsOne(s => s.ContactPerson).Property(p => p.FirstName).HasColumnName("FirstName").IsRequired();
            builder.OwnsOne(s => s.ContactPerson).Property(p => p.LastName).HasColumnName("LastName").IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.ModifiedBy).HasMaxLength(40);
            builder.Property(p => p.InactivatedBy).HasMaxLength(40);
        }
    }
}
