using MediatR;

namespace ClaimProcessing.Shared.Shipments.Queries.GetShipmentDetail
{
    public class GetShipmentDetailQuery : IRequest<ShipmentDetailVm>
    {
        public int ShipmentId { get; set; }
    }
}
