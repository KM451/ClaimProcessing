using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimProcessing.Application.Claims.Commands.DeleteClaim
{
    public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteClaimCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }


        public async Task Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);
            _context.Claims.Remove(claim);
            
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
