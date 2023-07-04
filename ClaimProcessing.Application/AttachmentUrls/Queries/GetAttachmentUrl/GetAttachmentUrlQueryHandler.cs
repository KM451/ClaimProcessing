using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrl
{
    public class GetAttachmentUrlQueryHandler : IRequestHandler<GetAttachmentUrlQuery, AttachmentUrlVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetAttachmentUrlQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<AttachmentUrlVm> Handle(GetAttachmentUrlQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.Id == request.AttachmentUrlId)
                .FirstOrDefaultAsync(cancellationToken);

            var attachmentVm = _mapper.Map<AttachmentUrlVm>(attachment);

            return attachmentVm;
        }
    }
}
