using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.UpdateRmaAvailability;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability
{
    public class UpdateRmaAvailabilityCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdateRmaAvailabilityCommand>
    {
        public async Task Handle(UpdateRmaAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                claim.RmaAvailable = request.RmaAvailable;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
