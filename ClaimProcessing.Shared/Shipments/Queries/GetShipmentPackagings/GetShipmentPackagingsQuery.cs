using MediatR;

namespace ClaimProcessing.Shared.Shipments.Queries.GetShipmentPackagings
{
    public class GetShipmentPackagingsQuery : IRequest<ShipmentPackagingsVm>
    {
        public int ShipmentId { get; set; }
    }
}
