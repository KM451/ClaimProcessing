using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.UpdateAttachmentUrl
{
    public class UpdateAttachmentUrlCommandHandler : IRequestHandler<UpdateAttachmentUrlCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdateAttachmentUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdateAttachmentUrlCommand request, CancellationToken cancellationToken)
        {
            var attachmentUrl = await _context.AttachmentUrls.Where(a => a.Id == request.AttachmentUrlId).FirstOrDefaultAsync(cancellationToken);

            if (attachmentUrl == null)
            {
                throw new NullException(request.AttachmentUrlId);
            }
            else
            {
                attachmentUrl.ClaimId = request.ClaimId;
                attachmentUrl.Path = request.Path;
       
                _context.AttachmentUrls.Update(attachmentUrl);
                await _context.SaveChangesAsync(cancellationToken);
            }

            
        }
    }
}
