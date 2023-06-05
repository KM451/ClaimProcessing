using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaims
{
    public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, ClaimsVm>
    {
        private readonly IClaimProcessingDbContext _context;

        public GetClaimsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }

        public async Task<ClaimsVm> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var claims = await _context.Claims
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            var claimsVm = new ClaimsVm
            {
                Claims = claims.Select(c => new ClaimsDto
                {
                    ClaimId = c.Id,
                    ClaimCreationDate = c.Created,
                    SupplierName = c.Supplier.Name,
                    ItemCode = c.ItemCode,
                    ClaimStatus = c.ClaimStatus
                }).ToList()
            };

            return claimsVm;

        }
    }
}
