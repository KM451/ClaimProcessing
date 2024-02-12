using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.UpdateClaimStatus;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus
{
    public class UpdateClaimStatusCommandHandler(IClaimProcessingDbContext _context, IBonfiClient _bonfi) : IRequestHandler<UpdateClaimStatusCommand, int>
    {

        public async Task<int> Handle(UpdateClaimStatusCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                if (request.ClaimStatus != -1)
                {
                    claim.ClaimStatus = request.ClaimStatus;
                }
                else
                {
                    if (claim.Remarks != null)
                    {
                        var claimBonfi = await _bonfi.GetClaim(claim.Remarks, new CancellationToken());
                        var decodedStatus = 0;
                        switch (claimBonfi.data.claim.status)
                        {
                            case "Open": decodedStatus = 1; break;
                            case "Assigned": decodedStatus = 2; break;
                            case "Work In Progress": decodedStatus = 3; break;
                            case "Managed": decodedStatus = 4; break;
                            case "Closed": decodedStatus = 5; break;
                            case "Cancelled": decodedStatus = 6; break;
                            default: decodedStatus = claim.ClaimStatus; break;
                        }
                        claim.ClaimStatus = decodedStatus;
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }  
                return claim.ClaimStatus;
            }
        }
    }
}