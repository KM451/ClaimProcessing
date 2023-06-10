using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;

namespace ClaimProcessing.Application.Packagings.Commands.CreatePackaging
{
    public class CreatePackagingCommandHandler : IRequestHandler<CreatePackagingCommand, int>
    {

        private readonly IClaimProcessingDbContext _context;
        public CreatePackagingCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreatePackagingCommand request, CancellationToken cancellationToken)
        {
            Packaging packaging = new()
            {
                Type = request.Type,
                Dimensions = new Dimensions(request.Height, request.Width, request.Depth),
                Weight = request.Weight,
                Notes = request.Notes,
                ShipmentId = request.ShipmentId,
            };
            _context.Packagings.Add(packaging);

            await _context.SaveChangesAsync(cancellationToken);
            return packaging.Id;
        }
    }
}
