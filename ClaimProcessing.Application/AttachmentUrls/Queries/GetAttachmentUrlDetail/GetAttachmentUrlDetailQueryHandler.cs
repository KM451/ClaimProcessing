using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Queries.GetAttachmentUrlDetail
{
    public class GetAttachmentUrlDetailQueryHandler : IRequestHandler<GetAttachmentUrlDetailQuery, AttachmentUrlDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetAttachmentUrlDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<AttachmentUrlDetailVm> Handle(GetAttachmentUrlDetailQuery request, CancellationToken cancellationToken)
        {
            var attachment = await _context.AttachmentUrls
                .Where(a => a.StatusId != 0 && a.Id == request.AttachmentUrlId)
                .FirstOrDefaultAsync(cancellationToken);

            var attachmentVm = _mapper.Map<AttachmentUrlDetailVm>(attachment);

            return attachmentVm;
        }
    }
}
