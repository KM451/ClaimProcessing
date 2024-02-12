using MediatR;

namespace ClaimProcessing.Shared.AttachmentUrls.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommand : IRequest<int>
    {
        public int ClaimId { get; set; }
        public string Path { get; set; }
    }
}
