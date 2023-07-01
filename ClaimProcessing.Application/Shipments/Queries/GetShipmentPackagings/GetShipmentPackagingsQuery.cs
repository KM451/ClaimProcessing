using MediatR;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings
{
    public class GetShipmentPackagingsQuery : IRequest<ShipmentPackagingsVm>
    {
        public int ShipmentId { get; set; }
    }
}
