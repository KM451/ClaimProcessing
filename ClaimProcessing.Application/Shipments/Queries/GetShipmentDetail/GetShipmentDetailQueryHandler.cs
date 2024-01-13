using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Shipments.Queries.GetShipmentDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail
{
    public class GetShipmentDetailQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetShipmentDetailQuery, ShipmentDetailVm>
    {
        public async Task<ShipmentDetailVm> Handle(GetShipmentDetailQuery request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments
                .Where(s => s.StatusId != 0 && s.Id == request.ShipmentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (shipment == null) 
                return null;

            var shipmentVm = Map(shipment);
            
            return shipmentVm;
        }

        private static ShipmentDetailVm Map(Shipment shipment)
        {
            return new ShipmentDetailVm
            {
                ShipmentDate = shipment.ShipmentDate,
                SupplierID = shipment.SupplierId,
                Speditor = shipment.Speditor,
                ShippingDocumentNo = shipment.ShippingDocumentNo,
                TotalWeight = shipment.TotalWeight
            };
        }
    }
}
