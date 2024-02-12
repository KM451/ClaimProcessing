using MediatR;

namespace ClaimProcessing.Shared.AttachmentUrls.Commands.DeleteAttachmentUrl
{
    public class DeleteAttachmentUrlCommand : IRequest
    {
        public int AttachmentUrlId { get; set; }
    }
}
