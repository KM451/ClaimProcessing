using MediatR;

namespace ClaimProcessing.Shared.Suppliers.Queries.GetSupplierShipments
{
    public class GetSupplierShipmentsQuery : IRequest<SupplierShipmentsVm>
    {
        public int SupplierId { get; set; }
        public string? Filter { get; set; }
    }
}
