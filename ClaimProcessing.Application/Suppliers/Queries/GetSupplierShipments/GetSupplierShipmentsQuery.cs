using MediatR;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments
{
    public class GetSupplierShipmentsQuery : IRequest<SupplierShipmentsVm>
    {
        public int SupplierId { get; set; }
    }
}
