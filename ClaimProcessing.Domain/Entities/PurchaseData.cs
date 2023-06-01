using ClaimProcessing.Domain.Common;

namespace ClaimProcessing.Domain.Entities
{
    public class PurchaseData : AuditableEntity
    {
        public string PurchaseInvoiceNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? InternalDocNo { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
