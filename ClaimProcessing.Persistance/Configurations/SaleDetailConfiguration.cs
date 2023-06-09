﻿using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimProcessing.Persistance.Configurations
{
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.SaleInvoiceNo).HasDefaultValue("").HasMaxLength(40).IsRequired();
            builder.Property(p => p.SaleDate).HasDefaultValue(DateTime.MinValue).IsRequired();
            builder.Property(p => p.ClaimId).IsRequired();
            builder.Property(p => p.CreatedBy).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Created).IsRequired();
            builder.Property(p => p.ModifiedBy).HasMaxLength(40);
            builder.Property(p => p.InactivatedBy).HasMaxLength(40);
        }
    }
}
