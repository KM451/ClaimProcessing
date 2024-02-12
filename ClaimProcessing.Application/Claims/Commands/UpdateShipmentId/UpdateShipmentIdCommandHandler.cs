using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Commands.UpdateShipmentId;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Commands.UpdateShipmentId
{
    public class UpdateShipmentIdCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdateShipmentIdCommand>
    {
        public async Task Handle(UpdateShipmentIdCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.Claims.Where(c => c.StatusId != 0 && c.Id == request.ClaimId).FirstOrDefaultAsync(cancellationToken);

            if (claim == null)
            {
                throw new NullException(request.ClaimId);
            }
            else
            {
                claim.ShipmentId = request.ShipmentId;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
