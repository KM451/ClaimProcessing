namespace ClaimProcessing.Api
{
    public class ClaimsForView
    {
        public int Id { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public int SupplierId { get; set; }
        public string? Customer { get; set; }
        public string? SaleInvoiceNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? PurchaseInvoiceNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? InternalDocNo { get; set; }
        public string? ItemName { get; set; }
        public string? ClaimDescription { get; set; }
        public string? Remarks { get; set; }
        public string? ClaimStatus { get; set; }
        public bool RmaAvailable { get; set; } = false;
        public int? ShipmentId { get; set; }
        public List<string>? Serials { get; private set; } = new List<string>();
        public List<string>? FotoUrls { get; private set; } = new List<string>();
        public List<string>? AttachmentUrls { get; private set; } = new List<string>();


    }
}
