using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentsUrlsClaim
{
    public class GetAttachmentsUrlsClaimQueryHandler : IRequestHandler<GetAttachmentsUrlsClaimQuery, AttachmentsUrlsClaimVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetAttachmentsUrlsClaimQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<AttachmentsUrlsClaimVm> Handle(GetAttachmentsUrlsClaimQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var attachmentsVm = _mapper.Map<AttachmentsUrlsClaimVm>(attachments);

            return attachmentsVm;
        }
    }
}
