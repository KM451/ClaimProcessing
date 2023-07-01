using AutoMapper;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateClaimCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Include(x => x.SaleDetail).Include(x => x.PurchaseDetail).Where(c => c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                _mapper.Map(request, claim);
                _mapper.Map(request, claim.SaleDetail);
                _mapper.Map(request, claim.PurchaseDetail);

                await _context.SaveChangesAsync(cancellationToken);
            }

            
        }
    }
}
