using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class GetAllClaimsShortQueryHandler : IRequestHandler<GetAllClaimsShortQuery, AllClaimsShortVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetAllClaimsShortQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task<AllClaimsShortVm> Handle(GetAllClaimsShortQuery request, CancellationToken cancellationToken)
        {
            var claims = await _context.Claims
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            var claimsVm = _mapper.Map<AllClaimsShortVm>(claims);

            return claimsVm;

        }
    }
}
