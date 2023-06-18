using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommandHandler : IRequestHandler<CreateFotoUrlCommand, int>
    {
        private readonly IClaimProcessingDbContext _context;
        public CreateFotoUrlCommandHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<int> Handle(CreateFotoUrlCommand request, CancellationToken cancellationToken)
        {
            FotoUrl fotoUrl = new()
            {
                Path = request.Path,
                ClaimId = request.ClaimId
            };
            _context.FotoUrls.Add(fotoUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return fotoUrl.Id;
        }
    }
}
