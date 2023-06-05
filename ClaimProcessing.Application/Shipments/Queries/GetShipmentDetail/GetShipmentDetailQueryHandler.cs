using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail
{
    public class GetShipmentDetailQueryHandler : IRequestHandler<GetShipmentDetailQuery, ShipmentDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetShipmentDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<ShipmentDetailVm> Handle(GetShipmentDetailQuery request, CancellationToken cancellationToken)
        {
            var shipment = _context.Shipments
                .Where(p => p.StatusId != 0 && p.Id == request.ShipmentId)
                .FirstOrDefaultAsync(cancellationToken);

            var shipmentVm = new ShipmentDetailVm
            {
                //ShipmentDate = shipment.ShipmentDate,
                //SupplierName = shipment.

            };

            return shipmentVm;
        }
    }
}
