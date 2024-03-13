using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.AttachmentUrls.Commands.DeleteAttachmentUrl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.DeleteAttachmentUrl
{
    public class DeleteAttachmentUrlCommandHandler : IRequestHandler<DeleteAttachmentUrlCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteAttachmentUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(DeleteAttachmentUrlCommand request, CancellationToken cancellationToken)
        {
            var attachmentUrl = await _context.AttachmentUrls.Where(a => a.StatusId != 0 && a.Id == request.AttachmentUrlId).FirstOrDefaultAsync(cancellationToken);
            if (attachmentUrl == null)
            {
                throw new NullException(request.AttachmentUrlId);
            }

            _context.AttachmentUrls.Remove(attachmentUrl);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
