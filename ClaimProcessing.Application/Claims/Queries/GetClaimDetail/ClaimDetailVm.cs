namespace ClaimProcessing.Application.Claims.Queries.GetClaimDetail
{
    public class ClaimDetailVm
    {
        public int ClaimId { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string SupplierName { get; set; }
        public string? CustomerName { get; set; }
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
        public DateTime? ShipmentDate { get; set; }
        

    }
}
