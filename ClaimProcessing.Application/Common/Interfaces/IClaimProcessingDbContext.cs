using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Common.Interfaces
{
    public interface IClaimProcessingDbContext
    {
        DbSet<Claim> Claims { get; set; }
        DbSet<Supplier> Supliers { get; set; }
        DbSet<Shipment> Shipments { get; set; }
        DbSet<Packaging> Packagings { get; set; }
        DbSet<SaleDetail> SaleDetails { get; set; }
        DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        DbSet<FotoUrl> FotoUrls { get; set; }
        DbSet<AttachmentUrl> AttachmentUrls { get; set; }
        DbSet<SerialNumber> SerialNumbers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
