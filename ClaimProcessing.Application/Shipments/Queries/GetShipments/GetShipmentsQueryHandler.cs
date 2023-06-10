using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class GetShipmentsQueryHandler : IRequestHandler<GetShipmentsQuery, ShipmentsVm>
    {
        private readonly IClaimProcessingDbContext _context;

        public GetShipmentsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<ShipmentsVm> Handle(GetShipmentsQuery request, CancellationToken cancellationToken)
        {
            var shipments = await _context.Shipments
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            var shipmentsVm = new ShipmentsVm
            {
                Shipments = shipments.Select(s => new ShipmentsDto
                {
                    ShipmentId = s.Id,
                    ShipmentDate = s.ShipmentDate,
                    SupplierName = s.Supplier.Name
                }).ToList()
            };
            return shipmentsVm;
        }
    }
}
