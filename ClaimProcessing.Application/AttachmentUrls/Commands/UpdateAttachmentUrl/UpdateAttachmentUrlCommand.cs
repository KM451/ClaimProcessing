using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.UpdateAttachmentUrl
{
    public class UpdateAttachmentUrlCommand : IRequest
    {
        public int AttachmentUrlId { get; set; }
        public int ClaimId { get; set; }
        public string Path { get; set; }
    }
}
