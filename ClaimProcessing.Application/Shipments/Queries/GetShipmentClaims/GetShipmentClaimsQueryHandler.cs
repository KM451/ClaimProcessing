using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Shipments.Queries.GetShipmentClaims;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims
{
    public class GetShipmentClaimsQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetShipmentClaimsQuery, ShipmentClaimsVm>
    {
        public async Task<ShipmentClaimsVm> Handle(GetShipmentClaimsQuery request, CancellationToken cancellationToken)
        {
            var claimSupplier = await _context.Claims
                .Where(p => p.StatusId != 0 && p.ShipmentId == request.ShipmentId)
                .ToListAsync(cancellationToken);

            if(claimSupplier == null) { return null; }

            var claimShipmentVm = new ShipmentClaimsVm
            {
                ShipmentClaims = claimSupplier.Select(src => Map(src)).ToList()
            };
            return claimShipmentVm;
        }
        private static ShipmentClaimsDto Map(Claim claim)
        {
            return new ShipmentClaimsDto
            {
                ClaimId = claim.Id,
                OwnerType = claim.OwnerType,
                ClaimType = claim.ClaimType,
                ItemCode = claim.ItemCode,
                Qty = claim.Qty,
                CustomerName = claim.CustomerName,
                ItemName = claim.ItemName,
                ShipmentId = claim.ShipmentId ?? 0
            };
        }
    }
}
