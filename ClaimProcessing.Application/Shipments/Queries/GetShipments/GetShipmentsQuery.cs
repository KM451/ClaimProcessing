using MediatR;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class GetShipmentsQuery : IRequest<ShipmentsVm>
    {
    }
}
