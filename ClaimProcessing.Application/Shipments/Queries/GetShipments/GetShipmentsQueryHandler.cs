using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Shipments.Queries.GetShipments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class GetShipmentsQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetShipmentsQuery, ShipmentsVm>
    {
        public async Task<ShipmentsVm> Handle(GetShipmentsQuery request, CancellationToken cancellationToken)
        {
            var shipments = await _context.Shipments
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            if(shipments == null) { return null; }

            var shipmentsVm = new ShipmentsVm
            {
                Shipments = shipments.Select(src => Map(src)).ToList()
            };

            return shipmentsVm;
        }

        private static ShipmentsDto Map(Shipment shipment) => new ShipmentsDto
            {
                ShipmentId = shipment.Id,
                ShipmentDate = shipment.ShipmentDate,
                SupplierName = shipment.Supplier.Name
            };
        
    }
}
