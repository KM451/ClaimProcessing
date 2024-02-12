using MediatR;

namespace ClaimProcessing.Shared.Claims.Queries.GetClaimFotosUrls
{
    public class GetClaimFotoUrlsQuery : IRequest<ClaimFotoUrlsVm>
    {
        public int ClaimId { get; set; }
    }
}
