using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotosUrlsClaim
{
    public class GetFotosUrlsClaimQueryHandler : IRequestHandler<GetFotosUrlsClaimQuery, FotosUrlsClaimVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetFotosUrlsClaimQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<FotosUrlsClaimVm> Handle(GetFotosUrlsClaimQuery request, CancellationToken cancellationToken)
        {
            var fotos = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var fotosVm = new FotosUrlsClaimVm
            {
                FotoUrls = fotos.Select(a => new FotosUrlsClaimDto
                {
                    FotoUrlId = a.Id,
                    Path = a.Path
                }).ToList()
            };

            return fotosVm;
        }
    }
}
