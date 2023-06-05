namespace ClaimProcessing.Api
{
    public class ShipmentForView
    {
        public int Id { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int SupplierId { get; set; }
        public double TotalWeight { get; set; }
        public string? Speditor { get; set; }
        public string? ShippingDocumentNo { get; set; }
        public List<PackagingForView> PackagingDetails { get; set; }

    }
}
