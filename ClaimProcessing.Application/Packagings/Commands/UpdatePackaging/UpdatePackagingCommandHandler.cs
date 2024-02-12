using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Shared.Packagings.Commands.UpdatePackaging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<UpdatePackagingCommand, int>
    {
        public async Task<int> Handle(UpdatePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings.Where(p => p.Id == request.PackagingId).FirstOrDefaultAsync(cancellationToken);

            if (packaging == null)
            {
                packaging = Map(request); 
                _context.Packagings.Add(packaging);
            }
            else
            {
                packaging.Type = request.Type;
                packaging.Weight = request.Weight;
                packaging.Notes = request.Notes;
                packaging.ShipmentId = request.ShipmentId;
                packaging.Dimensions.Depth = request.Depth;
                packaging.Dimensions.Width = request.Width;
                packaging.Dimensions.Height = request.Height;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return packaging.Id;
        }
        private static Packaging Map(UpdatePackagingCommand command)
        {
            return new Packaging
            {
                Type = command.Type,
                Weight = command.Weight,
                Notes = command.Notes,
                ShipmentId = command.ShipmentId,
                Dimensions = new Dimensions
                {
                    Depth = command.Depth,
                    Width = command.Width,
                    Height = command.Height
                }
            };
        }
    }
}
