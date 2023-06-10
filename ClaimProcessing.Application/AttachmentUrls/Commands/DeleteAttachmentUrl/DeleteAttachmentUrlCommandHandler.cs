﻿using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.AttachmentUrls.Commands.DeleteAttachmentUrl
{
    public class DeleteAttachmentUrlCommandHandler : IRequestHandler<DeleteAttachmentUrlCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteAttachmentUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(DeleteAttachmentUrlCommand request, CancellationToken cancellationToken)
        {
            var attachmentUrl = await _context.AttachmentUrls.Where(a => a.Id == request.AttachmentUrlId).FirstOrDefaultAsync(cancellationToken);
            _context.AttachmentUrls.Remove(attachmentUrl);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
