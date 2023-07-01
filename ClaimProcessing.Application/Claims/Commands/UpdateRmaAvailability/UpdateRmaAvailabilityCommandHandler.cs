using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateRmaAvailability
{
    public class UpdateRmaAvailabilityCommandHandler : IRequestHandler<UpdateRmaAvailabilityCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public UpdateRmaAvailabilityCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task Handle(UpdateRmaAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                claim = _mapper.Map(request, claim);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
