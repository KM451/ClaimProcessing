using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Packagings.Queries.GetPackaging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackaging
{
    public class GetPackagingQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetPackagingQuery, PackagingVm>
    {
        public async Task<PackagingVm> Handle(GetPackagingQuery request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.Id == request.PackagingId)
                .FirstOrDefaultAsync(cancellationToken);

            if(packaging == null) { return null; }

            var packagingVm = Map(packaging);
            return packagingVm;
        }
        private static PackagingVm Map(Packaging packaging)
        {
            return new PackagingVm
            {
                Type = packaging.Type,
                Dimensions = packaging.Dimensions.ToString(),
                Weight = packaging.Weight,
                Notes = packaging.Notes,
                ShipmentId = packaging.ShipmentId
            };
        }
    }
}
