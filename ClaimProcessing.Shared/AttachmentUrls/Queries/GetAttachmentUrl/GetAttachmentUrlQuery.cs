using MediatR;

namespace ClaimProcessing.Shared.AttachmentUrls.Queries.GetAttachmentUrl
{
    public class GetAttachmentUrlQuery : IRequest<AttachmentUrlVm>
    {
        public int AttachmentUrlId { get; set; }
    }
}
