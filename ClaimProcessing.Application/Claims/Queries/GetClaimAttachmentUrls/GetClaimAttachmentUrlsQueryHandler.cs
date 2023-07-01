using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimAttachmentsUrls
{
    public class GetClaimAttachmentUrlsQueryHandler : IRequestHandler<GetClaimAttachmentUrlsQuery, ClaimAttachmentUrlsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetClaimAttachmentUrlsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ClaimAttachmentUrlsVm> Handle(GetClaimAttachmentUrlsQuery request, CancellationToken cancellationToken)
        {
            var attachments = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var attachmentsVm = new ClaimAttachmentUrlsVm
            {
                AttachmentUrls = attachments.Select(src => _mapper.Map<ClaimAttachmentUrlsDto>(src)).ToList()
            };

            return attachmentsVm;
        }
    }
}
