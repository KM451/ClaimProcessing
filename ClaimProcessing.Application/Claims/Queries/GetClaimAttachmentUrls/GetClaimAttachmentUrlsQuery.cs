using MediatR;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls
{
    public class GetClaimAttachmentUrlsQuery : IRequest<ClaimAttachmentUrlsVm>
    {
        public int ClaimId { get; set; }
    }
}
