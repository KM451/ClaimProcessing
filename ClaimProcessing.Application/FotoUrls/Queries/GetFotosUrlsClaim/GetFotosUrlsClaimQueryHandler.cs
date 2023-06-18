using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotosUrlsClaim
{
    public class GetFotosUrlsClaimQueryHandler : IRequestHandler<GetFotosUrlsClaimQuery, FotosUrlsClaimVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetFotosUrlsClaimQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<FotosUrlsClaimVm> Handle(GetFotosUrlsClaimQuery request, CancellationToken cancellationToken)
        {
            var fotos = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var fotosVm = _mapper.Map<FotosUrlsClaimVm>(fotos);

            return fotosVm;
        }
    }
}
