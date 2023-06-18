using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl
{
    public class DeleteFotoUrlCommandHandler : IRequestHandler<DeleteFotoUrlCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public DeleteFotoUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(DeleteFotoUrlCommand request, CancellationToken cancellationToken)
        {
            var fotoUrl = await _context.FotoUrls.Where(f => f.Id == request.FotoUrlId).FirstOrDefaultAsync(cancellationToken);
            _context.FotoUrls.Remove(fotoUrl);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
