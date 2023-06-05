using MediatR;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail
{
    public class GetShipmentDetailQuery : IRequest<ShipmentDetailVm>
    {
        public int ShipmentId { get; set; }
    }
}
