using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var claim = await _context.Claims.Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);
            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }

            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync(cancellationToken);

        }

    }
}
