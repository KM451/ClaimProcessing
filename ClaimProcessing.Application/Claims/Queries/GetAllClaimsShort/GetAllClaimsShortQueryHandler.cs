using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetAllClaimsShort
{
    public class GetAllClaimsShortQueryHandler : IRequestHandler<GetAllClaimsShortQuery, AllClaimsShortVm>
    {
        private readonly IClaimProcessingDbContext _context;

        public GetAllClaimsShortQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }

        public async Task<AllClaimsShortVm> Handle(GetAllClaimsShortQuery request, CancellationToken cancellationToken)
        {
            var claims = await _context.Claims
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            var claimsVm = new AllClaimsShortVm
            {
                Claims = claims.Select(c => new AllClaimsShortDto
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
