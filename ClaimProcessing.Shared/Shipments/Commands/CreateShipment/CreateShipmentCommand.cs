using MediatR;

namespace ClaimProcessing.Shared.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommand : IRequest<int>
    {
        public DateTime ShipmentDate { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }
        public int SupplierId { get; set; }
    }
}
