using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Shared.Packagings.Commands.CreatePackaging;
using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.CreatePackaging
{
    public class CreatePackagingCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<CreatePackagingCommand, int>
    {
        public async Task<int> Handle(CreatePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = Map(request);
            
            _context.Packagings.Add(packaging);

            await _context.SaveChangesAsync(cancellationToken);
            return packaging.Id;
        }

        private static Packaging Map(CreatePackagingCommand command)
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
