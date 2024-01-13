using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Shipments.Queries.GetShipmentPackagings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings
{
    public class GetShipmentPackagingsQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetShipmentPackagingsQuery, ShipmentPackagingsVm>
    {
        public async Task<ShipmentPackagingsVm> Handle(GetShipmentPackagingsQuery request, CancellationToken cancellationToken)
        {
            var packagings = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.ShipmentId == request.ShipmentId)
                .ToListAsync(cancellationToken);

            if (packagings == null) return null;

            var packagingsVm = new ShipmentPackagingsVm
            {
                Packagings = packagings.Select(src => Map(src)).ToList()
            };

            return packagingsVm;
        }

        private ShipmentPackagingsDto Map(Packaging packaging)
        {
            return new ShipmentPackagingsDto
            {
                Type = packaging.Type,
                Dimensions = packaging.Dimensions.ToString(),
                Weight = packaging.Weight
            };
        }
    }
}
