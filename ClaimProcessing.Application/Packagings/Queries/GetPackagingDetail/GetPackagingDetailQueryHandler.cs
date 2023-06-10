using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingDetail
{
    public class GetPackagingDetailQueryHandler : IRequestHandler<GetPackagingDetailQuery, PackagingDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetPackagingDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<PackagingDetailVm> Handle(GetPackagingDetailQuery request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.Id == request.PackagingId)
                .FirstOrDefaultAsync(cancellationToken);

            var packagingVm = new PackagingDetailVm
            {
                Type = packaging.Type,
                Dimensions = packaging.Dimensions.ToString(),
                Weight = packaging.Weight,
                Notes = packaging.Notes,
                ShipmentId = packaging.ShipmentId,
            };
            return packagingVm;
        }
    }
}
