using MediatR;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims
{
    public class GetShipmentClaimsQuery : IRequest<ShipmentClaimsVm>
    {
        public int ShipmentId { get; set; }
    }
}
