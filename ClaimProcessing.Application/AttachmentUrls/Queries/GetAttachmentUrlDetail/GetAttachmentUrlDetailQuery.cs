using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrlDetail
{
    public class GetAttachmentUrlDetailQuery : IRequest<AttachmentUrlDetailVm>
    {
        public int AttachmentUrlId { get; set; }
    }
}
