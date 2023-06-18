using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommandHandler : IRequestHandler<UpdatePackagingCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdatePackagingCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdatePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings.Where(p => p.Id == request.PackagingId).FirstOrDefaultAsync(cancellationToken);
            if (packaging == null)
            {
                throw new NullException(request.PackagingId);
            }
            else
            {
                packaging.Type = request.Type;
                packaging.Dimensions = new Dimensions(request.Height, request.Width, request.Depth);
                packaging.Weight = request.Weight;
                packaging.Notes = request.Notes;
                packaging.ShipmentId = request.ShipmentId;

                _context.Packagings.Update(packaging);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
