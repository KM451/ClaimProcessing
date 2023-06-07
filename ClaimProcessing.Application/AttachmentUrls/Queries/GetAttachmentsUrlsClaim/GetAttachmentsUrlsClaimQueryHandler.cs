using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentsUrlsClaim
{
    public class GetAttachmentsUrlsClaimQueryHandler : IRequestHandler<GetAttachmentsUrlsClaimQuery, AttachmentsUrlsClaimVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetAttachmentsUrlsClaimQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<AttachmentsUrlsClaimVm> Handle(GetAttachmentsUrlsClaimQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var attachmentsVm = new AttachmentsUrlsClaimVm
            {
                AttachmentUrls = attachments.Select(a => new AttachmentsUrlsClaimDto
                {
                    AttachmentUrlId = a.Id,
                    Path = a.Path
                }).ToList()
            };

            return attachmentsVm;
        }
    }
}
