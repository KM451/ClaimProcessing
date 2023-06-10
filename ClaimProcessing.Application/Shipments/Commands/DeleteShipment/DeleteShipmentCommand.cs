using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.DeleteShipment
{
    public class DeleteShipmentCommand : IRequest
    {
        public int ShipmentId { get; set; }
    }
}
