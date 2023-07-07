using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Commands.DeletePackaging
{
    public class DeletePackagingCommandHandler : IRequestHandler<DeletePackagingCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeletePackagingCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(DeletePackagingCommand request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings.Where(p => p.StatusId != 0 && p.Id == request.PackagingId).FirstOrDefaultAsync(cancellationToken);

            if (packaging == null)
            {
                throw new NullException(request.PackagingId);
            }

            var dimensions = new Dimensions(packaging.Dimensions.Height, packaging.Dimensions.Width, packaging.Dimensions.Depth);
            _context.Packagings.Remove(packaging);
            packaging.Dimensions = dimensions;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
