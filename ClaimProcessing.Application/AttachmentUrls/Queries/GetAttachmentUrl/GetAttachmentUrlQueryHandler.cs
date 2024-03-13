using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.AttachmentUrls.Queries.GetAttachmentUrl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl
{
    public class GetAttachmentUrlQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetAttachmentUrlQuery, AttachmentUrlVm>
    {
        public async Task<AttachmentUrlVm> Handle(GetAttachmentUrlQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.Id == request.AttachmentUrlId)
                .FirstOrDefaultAsync(cancellationToken);

            if (attachment == null) { return null; }

            var attachmentVm = new AttachmentUrlVm {Path = attachment.Path};

            return attachmentVm;
        }
        
    }
}
