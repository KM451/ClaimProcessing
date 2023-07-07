using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Commands.DeleteShipment
{
    public class DeleteShipmentCommandHandler : IRequestHandler<DeleteShipmentCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteShipmentCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(DeleteShipmentCommand request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments.Where(s => s.Id == request.ShipmentId).FirstOrDefaultAsync(cancellationToken);

            if (shipment == null)
            {
                throw new NullException(request.ShipmentId);
            }

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
