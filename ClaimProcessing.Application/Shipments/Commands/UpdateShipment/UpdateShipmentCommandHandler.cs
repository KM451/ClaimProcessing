using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Shipments.Commands.UpdateShipment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdateShipmentCommand, int>
    {

        public async Task<int> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments.Where(s => s.Id == request.ShipmentId).FirstOrDefaultAsync(cancellationToken);

            if (shipment == null)
            {
                shipment = Map(request);
                _context.Shipments.Add(shipment);
            }
            else
            {
                shipment.ShipmentDate = request.ShipmentDate;
                shipment.SupplierId = request.SupplierId;
                shipment.Speditor = request.Speditor;
                shipment.ShippingDocumentNo = request.ShippingDocumentNo;
                shipment.TotalWeight = request.TotalWeight;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return shipment.Id;
        }

        private static Shipment Map(UpdateShipmentCommand command)
        {
            return new Shipment
            {
                ShipmentDate = command.ShipmentDate,
                Speditor = command.Speditor,
                ShippingDocumentNo = command.ShippingDocumentNo,
                TotalWeight = command.TotalWeight,
                SupplierId = command.SupplierId
            };
        }
    }
}

