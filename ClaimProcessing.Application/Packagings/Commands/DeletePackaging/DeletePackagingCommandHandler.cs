using ClaimProcessing.Application.Common.Interfaces;
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
            var packaging = await _context.Packagings.Where(p => p.Id == request.PackagingId).FirstOrDefaultAsync(cancellationToken);
            _context.Packagings.Remove(packaging);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
