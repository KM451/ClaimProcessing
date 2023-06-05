using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimDetail
{
    public class GetClaimDetailQuery : IRequest<ClaimDetailVm>
    {
        public int ClaimId { get; set; }
    }
}
