using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Claims.Queries.GetClaimFotosUrls
{
    public class GetClaimFotoUrlsQueryHandler : IRequestHandler<GetClaimFotoUrlsQuery, ClaimFotoUrlsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetClaimFotoUrlsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ClaimFotoUrlsVm> Handle(GetClaimFotoUrlsQuery request, CancellationToken cancellationToken)
        {
            var fotos = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.ClaimId == request.ClaimId)
                .ToListAsync(cancellationToken);

            var fotosVm = new ClaimFotoUrlsVm
            {
                FotoUrls = fotos.Select(src => _mapper.Map<ClaimFotoUrlsDto>(src)).ToList()
            };

            return fotosVm;
        }
    }
}
