using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.CreateAttachmentUrl
{
    public class CreateAttachmentUrlCommandHandler : IRequestHandler<CreateAttachmentUrlCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public CreateAttachmentUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateAttachmentUrlCommand request, CancellationToken cancellationToken)
        {
            var attachmentUrl = _mapper.Map<AttachmentUrl>(request);
            
            _context.AttachmentUrls.Add(attachmentUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return attachmentUrl.Id;
        }
    }
}

