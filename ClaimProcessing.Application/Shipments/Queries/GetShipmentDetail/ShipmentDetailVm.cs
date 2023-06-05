namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail
{
    public class ShipmentDetailVm
    {
        public DateTime ShipmentDate { get; set; }
        public int SupplierName { get; set; }
        public string? Speditor { get; set; }
        public string? ShippingDocumentNo { get; set; }
        public double TotalWeight { get; set; }
    }
}
