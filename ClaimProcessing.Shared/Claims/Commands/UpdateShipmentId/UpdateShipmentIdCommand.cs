using MediatR;

namespace ClaimProcessing.Shared.Claims.Commands.UpdateShipmentId
{
    public class UpdateShipmentIdCommand :  IRequest
    {
        public int ClaimId { get; set; }
        public int ShipmentId { get; set; }

    }
}
