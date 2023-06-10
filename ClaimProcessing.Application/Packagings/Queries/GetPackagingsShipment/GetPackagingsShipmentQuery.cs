using MediatR;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingsShipment
{
    public class GetPackagingsShipmentQuery : IRequest<PackagingsShipmentVm>
    {
        public int ShipmentId { get; set; }
    }
}
