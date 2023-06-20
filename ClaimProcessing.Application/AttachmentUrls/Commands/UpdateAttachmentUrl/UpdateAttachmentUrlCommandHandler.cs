using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ClaimProcessing.Domain.Entities;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.UpdateAttachmentUrl
{
    public class UpdateAttachmentUrlCommandHandler : IRequestHandler<UpdateAttachmentUrlCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateAttachmentUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
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
                attachmentUrl = _mapper.Map<AttachmentUrl>(request);
                       
                _context.AttachmentUrls.Update(attachmentUrl);
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
