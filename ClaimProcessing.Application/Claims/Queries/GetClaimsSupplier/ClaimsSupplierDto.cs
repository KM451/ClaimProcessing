namespace ClaimProcessing.Application.Claims.Queries.GetClaimsSupplier
{
    public class ClaimsSupplierDto
    {
        public int ClaimId { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string SupplierName { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public int ClaimStatus { get; set; }
        public bool RmaAvailable { get; set; } = false;
        public DateTime? ShipmentDate { get; set; }
        

    }
}
