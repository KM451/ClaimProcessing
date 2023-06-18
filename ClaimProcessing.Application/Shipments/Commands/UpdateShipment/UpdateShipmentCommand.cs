using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommand : IRequest
    {
        public int ShipmentId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int SupplierID { get; set; }
        public string? Speditor { get; set; }
        public string? ShippingDocumentNo { get; set; }
        public decimal? TotalWeight { get; set; }
    }
}
