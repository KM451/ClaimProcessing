using MediatR;

namespace ClaimProcessing.Shared.Suppliers.Queries.GetSupplierClaims
{
    public class GetSupplierClaimsQuery : IRequest<SupplierClaimsVm>
    {
        public int SupplierId { get; set; }
        public string? Filter { get; set; }
    }
}
