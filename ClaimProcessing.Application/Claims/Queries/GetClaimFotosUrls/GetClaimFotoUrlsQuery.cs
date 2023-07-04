using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls
{
    public class GetClaimFotoUrlsQuery : IRequest<ClaimFotoUrlsVm>
    {
        public int ClaimId { get; set; }
    }
}
