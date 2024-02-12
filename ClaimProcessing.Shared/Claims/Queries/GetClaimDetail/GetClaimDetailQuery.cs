using MediatR;

namespace ClaimProcessing.Shared.Claims.Queries.GetClaimDetail
{
    public class GetClaimDetailQuery : IRequest<ClaimDetailVm>
    {
        public int ClaimId { get; set; }
    }
}
