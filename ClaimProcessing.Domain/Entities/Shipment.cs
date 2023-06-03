using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.Entities
{
    public class Shipment : AuditableEntity
    {
        public DateTime ShipmentDate { get; set; }
        public string? PackingDetails { get; set; }
        public decimal? TotalWeight { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<Claim> Claims { get; set; }
        public ICollection<Packaging> Packagings { get; set; }

    }
}
