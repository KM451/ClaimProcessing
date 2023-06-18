using MediatR;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierDetail
{
    public class GetSupplierDetailQuery : IRequest<SupplierDetailVm>
    {
        public int SupplierId { get; set; }
    }
}
