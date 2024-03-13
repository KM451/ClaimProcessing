namespace ClaimProcessing.Shared.Suppliers.Queries.GetSupplierShipments
{
    public class SupplierShipmentsDto
    {
        public DateTime ShipmentDate { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }

    }
}
