using ClaimProcessing.Application.Common.Exceptions;
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
            var fotoUrl = await _context.FotoUrls.Where(f => f.StatusId != 0 && f.Id == request.FotoUrlId).FirstOrDefaultAsync(cancellationToken);

            if (fotoUrl == null)
            {
                throw new NullException(request.FotoUrlId);
            }

            _context.FotoUrls.Remove(fotoUrl);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
