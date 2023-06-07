using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrlDetail
{
    public class GetFotoUrlDetailQueryHandler : IRequestHandler<GetFotoUrlDetailQuery, FotoUrlDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        public GetFotoUrlDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext)
        {
            _context = claimProcessingDbContext;
        }
        public async Task<FotoUrlDetailVm> Handle(GetFotoUrlDetailQuery request, CancellationToken cancellationToken)
        {
            var foto = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.Id == request.FotoUrlId)
                .FirstOrDefaultAsync(cancellationToken);
            var fotoVm = new FotoUrlDetailVm
            {
                Path = foto.Path
            };
            return fotoVm;
        }
    }
}
