using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.FotoUrls.Commands.DeleteFotoUrl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Commands.DeleteFotoUrl
{
    public class DeleteFotoUrlCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<DeleteFotoUrlCommand>
    {
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
