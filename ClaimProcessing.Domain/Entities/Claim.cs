using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.Entities
{
    public class Claim : AuditableEntity
    {
        public string ClaimNumber { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public string? ClaimDescription { get; set; }
        public string? Remarks { get; set; }
        public string? ClaimStatus { get; set; }
        public bool RmaAvailable { get; set; } = false;
        public int? ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<SerialNumber> SerialNumbers { get; set; }
        public ICollection<FotoUrl> FotoUrls { get; set;}
        public ICollection<AttachmentUrl> AttachmentUrls { get; set; }
        public SaleDetail SaleDetail { get; set; }
        public PurchaseDetail PurchaseDetail { get; set; }  

    }
}
