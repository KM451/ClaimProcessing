using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingsShipment
{
    public class GetPackagingsShipmentQueryHandler : IRequestHandler<GetPackagingsShipmentQuery, PackagingsShipmentVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetPackagingsShipmentQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<PackagingsShipmentVm> Handle(GetPackagingsShipmentQuery request, CancellationToken cancellationToken)
        {
            var packagings = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.ShipmentId== request.ShipmentId)
                .ToListAsync(cancellationToken);

            var packagingsVm = new PackagingsShipmentVm
            {
                Packagings = packagings.Select(p => new PackagingsShipmentDto
                {
                    Type = p.Type,
                    Dimensions = p.Dimensions.ToString(),
                    Weight = p.Weight,
                }).ToList()
            };
            return packagingsVm;
        }
    }
}
