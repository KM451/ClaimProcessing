using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Queries.GetClaimAttachmentUrls;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls
{
    public class GetClaimAttachmentUrlsQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetClaimAttachmentUrlsQuery, ClaimAttachmentUrlsVm>
    {
        public async Task<ClaimAttachmentUrlsVm> Handle(GetClaimAttachmentUrlsQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var attachmentsVm = new ClaimAttachmentUrlsVm
            {
                AttachmentUrls = attachments.Select(src => new ClaimAttachmentUrlsDto { Path = src.Path }).ToList()
            };

            return attachmentsVm;
        }
    }
}
