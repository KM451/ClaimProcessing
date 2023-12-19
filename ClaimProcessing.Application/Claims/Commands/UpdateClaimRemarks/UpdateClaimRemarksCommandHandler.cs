using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaimRemarks
{
    public class UpdateClaimRemarksCommandHandler(IClaimProcessingDbContext _context, IMapper _mapper) : IRequestHandler<UpdateClaimRemarksCommand, string>
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
                claim = _mapper.Map(request, claim);
                await _context.SaveChangesAsync(cancellationToken);
                return request.Remarks;
            }
        }
    }
}
