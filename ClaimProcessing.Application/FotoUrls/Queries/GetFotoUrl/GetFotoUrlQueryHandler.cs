using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Shared.FotoUrls.Queries.GetFotoUrl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl
{
    public class GetFotoUrlQueryHandler(IClaimProcessingDbContext _context) : IRequestHandler<GetFotoUrlQuery, FotoUrlVm>
    {
        public async Task<FotoUrlVm> Handle(GetFotoUrlQuery request, CancellationToken cancellationToken)
        {
            var foto = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.Id == request.FotoUrlId)
                .FirstOrDefaultAsync(cancellationToken);

            if(foto == null)
            {
                return null;
            }

            var fotoVm = Map(foto);
            return fotoVm;
        }

        private static FotoUrlVm Map(FotoUrl query)
        {
            return new FotoUrlVm
            {
                Path = query.Path
            };
        }
    }
}
