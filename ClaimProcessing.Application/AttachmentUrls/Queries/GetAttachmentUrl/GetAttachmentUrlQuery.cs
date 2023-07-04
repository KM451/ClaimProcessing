using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl
{
    public class GetAttachmentUrlQuery : IRequest<AttachmentUrlVm>
    {
        public int AttachmentUrlId { get; set; }
    }
}
