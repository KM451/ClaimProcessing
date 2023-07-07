using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public UpdateClaimCommandHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Include(x => x.SaleDetail).Include(x => x.PurchaseDetail).Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                claim = _mapper.Map<Claim>(request);
                claim.Id = 0;
                claim.SaleDetail = _mapper.Map<SaleDetail>(request);
                claim.PurchaseDetail = _mapper.Map<PurchaseDetail>(request);
                _context.Claims.Add(claim);
            }
            else
            {
                _mapper.Map(request, claim);
                _mapper.Map(request, claim.SaleDetail);
                _mapper.Map(request, claim.PurchaseDetail);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return claim.Id;

        }
    }
}
