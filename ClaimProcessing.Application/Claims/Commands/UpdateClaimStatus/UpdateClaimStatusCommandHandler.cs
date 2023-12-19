using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimStatus
{
    public class UpdateClaimStatusCommandHandler(IClaimProcessingDbContext _context, IMapper _mapper, IBonfiClient _bonfi) : IRequestHandler<UpdateClaimStatusCommand, int>
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
                    claim = _mapper.Map(request, claim);
                }
                else
                {
                    if (claim.Remarks != null)
                    {
                        var claimBonfi = await _bonfi.GetClaim(claim.Remarks, new CancellationToken());
                        var decodedStatus = 0;
                        switch (claimBonfi.data.claim.status)
                        {
                            case "Open": decodedStatus = 10; break;
                            case "Assigned": decodedStatus = 11; break;
                            case "Work In Progress": decodedStatus = 12; break;
                            case "Managed": decodedStatus = 13; break;
                            case "Closed": decodedStatus = 14; break;
                            case "Cancelled": decodedStatus = 15; break;
                            default: decodedStatus = claim.ClaimStatus; break;
                        }
                        request.ClaimStatus = decodedStatus;
                        claim = _mapper.Map(request, claim);
                    }
                    await _context.SaveChangesAsync(cancellationToken);
                }  
                return request.ClaimStatus;
            }
        }
    }
}