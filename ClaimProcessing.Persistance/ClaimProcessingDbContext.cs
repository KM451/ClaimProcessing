using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Common;
using ClaimProcessing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ClaimProcessing.Persistance
{
    public class ClaimProcessingDbContext : DbContext, IClaimProcessingDbContext
    {
        private readonly IDateTime _dateTime;
        public ClaimProcessingDbContext(DbContextOptions<ClaimProcessingDbContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }
        public ClaimProcessingDbContext(DbContextOptions<ClaimProcessingDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<FotoUrl> FotoUrls { get; set; }
        public DbSet<AttachmentUrl> AttachmentUrls { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {        
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) 
        { 
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = string.Empty; 
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.StatusId = 1;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = _dateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = _dateTime.Now;
                        entry.Entity.Inactivated = _dateTime.Now;
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
