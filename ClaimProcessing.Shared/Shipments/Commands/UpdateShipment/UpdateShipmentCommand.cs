using MediatR;

namespace ClaimProcessing.Shared.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommand : IRequest<int>
    {
        public int ShipmentId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public int SupplierId { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }
    }
}
