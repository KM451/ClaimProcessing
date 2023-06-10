using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrlDetail
{
    public class GetAttachmentUrlDetailQueryHandler : IRequestHandler<GetAttachmentUrlDetailQuery, AttachmentUrlDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetAttachmentUrlDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<AttachmentUrlDetailVm> Handle(GetAttachmentUrlDetailQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.Id == request.AttachmentUrlId)
                .FirstOrDefaultAsync(cancellationToken);
            var attachmentVm = new AttachmentUrlDetailVm
            {
                Path = attachment.Path
            };
            return attachmentVm;
        }
    }
}
