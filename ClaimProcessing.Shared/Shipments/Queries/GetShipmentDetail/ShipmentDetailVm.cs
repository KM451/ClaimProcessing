namespace ClaimProcessing.Shared.Shipments.Queries.GetShipmentDetail
{
    public class ShipmentDetailVm
    {
        public DateTime ShipmentDate { get; set; }
        public int SupplierID { get; set; }
        public string? Speditor { get; set; }
        public string? ShippingDocumentNo { get; set; }
        public decimal? TotalWeight { get; set; }
    }
}
