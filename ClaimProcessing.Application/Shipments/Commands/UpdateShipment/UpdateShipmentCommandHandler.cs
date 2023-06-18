using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdateShipmentCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments.Where(s => s.Id == request.ShipmentId).FirstOrDefaultAsync(cancellationToken);
            if (shipment == null)
            {
                throw new NullException(request.ShipmentId);
            }
            else
            {
                shipment.ShipmentDate = request.ShipmentDate;
                shipment.SupplierId = request.SupplierID;
                shipment.Speditor = request.Speditor;
                shipment.ShippingDocumentNo = request.ShippingDocumentNo;
                shipment.TotalWeight = request.TotalWeight;

                _context.Shipments.Update(shipment);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
        }
    }
}
