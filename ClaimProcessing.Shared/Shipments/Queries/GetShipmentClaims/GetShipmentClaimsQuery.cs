using MediatR;

namespace ClaimProcessing.Shared.Shipments.Queries.GetShipmentClaims
{
    public class GetShipmentClaimsQuery : IRequest<ShipmentClaimsVm>
    {
        public int ShipmentId { get; set; }
    }
}
