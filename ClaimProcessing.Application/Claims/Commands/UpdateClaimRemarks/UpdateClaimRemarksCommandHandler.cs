using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaimRemarks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimRemarks
{
    public class UpdateClaimRemarksCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdateClaimRemarksCommand, string>
    {
        public async Task<string> Handle(UpdateClaimRemarksCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                claim.Remarks = request.Remarks;
                await _context.SaveChangesAsync(cancellationToken);
                return request.Remarks;
            }
        }
    }
}
