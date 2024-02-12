namespace ClaimProcessing.Shared.Shipments.Queries.GetShipmentClaims
{
    public class ShipmentClaimsDto
    {
        public int ClaimId { get; set; }
        public string OwnerType { get; set; }
        public string ClaimType { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string? CustomerName { get; set; }
        public string? ItemName { get; set; }
        public int ShipmentId { get; set; }
    }
}
