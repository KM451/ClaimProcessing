using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentsUrlsClaim
{
    public class GetAttachmentsUrlsClaimQuery : IRequest<AttachmentsUrlsClaimVm>
    {
        public int ClaimId { get; set; }
    }
}
