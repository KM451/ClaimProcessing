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
            var shipment = await _context.Shipments
                .Where(s => s.StatusId != 0 && s.Id == request.ShipmentId)
                .FirstOrDefaultAsync(cancellationToken);

            var shipmentVm = new ShipmentDetailVm
            {
                ShipmentDate = shipment.ShipmentDate,
                SupplierID = shipment.SupplierId,
                Speditor = shipment?.Speditor ?? "",
                ShippingDocumentNo = shipment?.ShippingDocumentNo ?? "",
                TotalWeight = shipment?.TotalWeight ?? 0,
            };

            return shipmentVm;
        }
    }
}
