using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.DeleteAttachmentUrl
{
    public class DeleteAttachmentUrlCommand : IRequest
    {
        public int AttachmentUrlId { get; set; }
    }
}
