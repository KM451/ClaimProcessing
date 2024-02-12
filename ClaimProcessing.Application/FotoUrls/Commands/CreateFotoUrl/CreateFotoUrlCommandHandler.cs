using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;
using MediatR;

namespace ClaimProcessing.Application.FotoUrls.Commands.CreateFotoUrl
{
    public class CreateFotoUrlCommandHandler(IClaimProcessingDbContext _context) : IRequestHandler<CreateFotoUrlCommand, int>
    {
        public async Task<int> Handle(CreateFotoUrlCommand request, CancellationToken cancellationToken)
        {
            var fotoUrl = Map(request);
            
            _context.FotoUrls.Add(fotoUrl);
            await _context.SaveChangesAsync(cancellationToken);
            return fotoUrl.Id;
        }
        private static FotoUrl Map(CreateFotoUrlCommand command)
        {
            return new FotoUrl
            {
                Path = command.Path,
                ClaimId = command.ClaimId
            };
        }
    }
}
