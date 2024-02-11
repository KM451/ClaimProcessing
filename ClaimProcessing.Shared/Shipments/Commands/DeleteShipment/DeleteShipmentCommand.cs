using MediatR;

namespace ClaimProcessing.Shared.Shipments.Commands.DeleteShipment
{
    public class DeleteShipmentCommand : IRequest
    {
        public int ShipmentId { get; set; }
    }
}
