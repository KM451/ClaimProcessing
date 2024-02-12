using MediatR;

namespace ClaimProcessing.Shared.Claims.Queries.GetClaimAttachmentUrls
{
    public class GetClaimAttachmentUrlsQuery : IRequest<ClaimAttachmentUrlsVm>
    {
        public int ClaimId { get; set; }
    }
}
