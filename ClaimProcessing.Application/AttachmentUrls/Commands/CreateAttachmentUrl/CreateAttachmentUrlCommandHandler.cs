using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.AttachmentUrls.Commands.CreateAttachmentUrl;
using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<CreateAttachmentUrlCommand, int>
    {
        public async Task<int> Handle(CreateAttachmentUrlCommand request, CancellationToken cancellationToken)
        {
            var attachmentUrl = new AttachmentUrl
            {
                Path = request.Path,
                ClaimId = request.ClaimId
            };
            
            _context.AttachmentUrls.Add(attachmentUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return attachmentUrl.Id;
        }
    }
}

