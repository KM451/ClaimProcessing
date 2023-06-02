using ClaimProcessing.Domain.Common;
using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Persistance
{
    public class ClaimProcessingDbContext : DbContext
    {
        public ClaimProcessingDbContext(DbContextOptions<ClaimProcessingDbContext> options) : base(options)
        {
            
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Supplier> Supliers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<FotoUrl> FotoUrls { get; set; }
        public DbSet<AttachmentUrl> AttachmentUrls { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().OwnsOne(s => s.Address);
            modelBuilder.Entity<Supplier>().OwnsOne(s => s.ContactPerson);
            modelBuilder.Entity<Packaging>().OwnsOne(p => p.Dimensions);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) 
        { 
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = string.Empty; 
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.StatusId = 1;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = DateTime.Now;
                        entry.Entity.Inactivated = DateTime.Now;
                        entry.Entity.InactivatedBy = string.Empty;
                        entry.Entity.StatusId = 0;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
