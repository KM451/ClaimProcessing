using MediatR;

namespace ClaimProcessing.Shared.Suppliers.Queries.GetSupplierDetail
{
    public class GetSupplierDetailQuery : IRequest<SupplierDetailVm>
    {
        public int SupplierId { get; set; }
    }
}
