using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimsSupplier
{
    public class GetCliamsSupplierQueryHandler : IRequestHandler<GetClaimsSupplierQuery, ClaimsSupplierVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetCliamsSupplierQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<ClaimsSupplierVm> Handle(GetClaimsSupplierQuery request, CancellationToken cancellationToken)
        {
            var claimSupplier = await _context.Claims
                .Where(p => p.StatusId != 0 && p.SupplierId == request.SupplierId)
                .Include(p => p.Shipment)
                .ToListAsync(cancellationToken);

            var claimSupplierVm = new ClaimsSupplierVm
            {
                ClaimsSupplier = claimSupplier.Select(c => new ClaimsSupplierDto
                {
                    ClaimId = c.Id,
                    OwnerType = c.OwnerType,
                    ClaimType = c.ClaimType,
                    ItemCode = c.ItemCode,
                    Qty = c.Qty,
                    CustomerName = c.CustomerName,
                    ItemName = c.ItemName,
                    ClaimStatus = c.ClaimStatus,
                    RmaAvailable = c.RmaAvailable,
                    ShipmentDate = c.Shipment.ShipmentDate,
                }).ToList()

            };

            return claimSupplierVm;
        }
    }
}
