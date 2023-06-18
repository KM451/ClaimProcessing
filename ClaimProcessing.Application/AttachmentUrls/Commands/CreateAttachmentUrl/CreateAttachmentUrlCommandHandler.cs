using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommandHandler : IRequestHandler<CreateAttachmentUrlCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        public CreateAttachmentUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreateAttachmentUrlCommand request, CancellationToken cancellationToken)
        {
            AttachmentUrl attachmentUrl = new()
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

