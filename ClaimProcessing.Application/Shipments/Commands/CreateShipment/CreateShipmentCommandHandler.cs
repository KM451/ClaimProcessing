using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.Shipments.Commands.CreateShipment
{
    public class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        public CreateShipmentCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            Shipment shipment = new()
            {
                ShipmentDate = request.ShipmentDate,
                SupplierId = request.SupplierID
            };
            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync(cancellationToken);
            return shipment.Id;
        }
    }
}
