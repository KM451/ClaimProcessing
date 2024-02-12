using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Shared.Claims.Queries.GetClaimFotosUrls;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls
{
    public class GetClaimFotoUrlsQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetClaimFotoUrlsQuery, ClaimFotoUrlsVm>
    {
        public async Task<ClaimFotoUrlsVm> Handle(GetClaimFotoUrlsQuery request, CancellationToken cancellationToken)
        {
            var fotos = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var fotosVm = new ClaimFotoUrlsVm
            {
                FotoUrls = fotos.Select(src => new ClaimFotoUrlsDto { Path = src.Path, Id = src.Id }).ToList()
            };

            return fotosVm;
        }
    }
}
