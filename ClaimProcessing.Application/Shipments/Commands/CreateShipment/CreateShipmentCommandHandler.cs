using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.Shipments.Commands.CreateShipment;
using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommandHandler(IClaimProcessingDbContext _context) 
        : IRequestHandler<CreateShipmentCommand, int>
    {

        public async Task<int> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = Map(request);
            
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync(cancellationToken);
            return shipment.Id;
        }

        private static Shipment Map(CreateShipmentCommand command)
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
