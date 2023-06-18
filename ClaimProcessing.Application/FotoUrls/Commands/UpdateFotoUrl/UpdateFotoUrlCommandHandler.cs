using ClaimProcessing.Application.Common.Exceptions;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Commands.UpdateFotoUrl
{
    public class UpdateFotoUrlCommandHandler : IRequestHandler<UpdateFotoUrlCommand>
    {
        private readonly IClaimProcessingDbContext _context;
        public UpdateFotoUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task Handle(UpdateFotoUrlCommand request, CancellationToken cancellationToken)
        {
            var fotoUrl = await _context.FotoUrls.Where(f => f.Id == request.FotoUrlId).FirstOrDefaultAsync(cancellationToken);

            if (fotoUrl == null)
            {
                throw new NullException(request.FotoUrlId);
            }
            else
            {
                fotoUrl.ClaimId = request.ClaimId;
                fotoUrl.Path = request.Path;

                _context.FotoUrls.Update(fotoUrl);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
