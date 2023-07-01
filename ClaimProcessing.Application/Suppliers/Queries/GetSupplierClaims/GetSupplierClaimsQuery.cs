using MediatR;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims
{
    public class GetSupplierClaimsQuery : IRequest<SupplierClaimsVm>
    {
        public int SupplierId { get; set; }
    }
}
