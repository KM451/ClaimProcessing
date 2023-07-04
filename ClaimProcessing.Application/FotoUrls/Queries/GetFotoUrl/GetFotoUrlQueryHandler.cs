using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrl
{
    public class GetFotoUrlQueryHandler : IRequestHandler<GetFotoUrlQuery, FotoUrlVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetFotoUrlQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<FotoUrlVm> Handle(GetFotoUrlQuery request, CancellationToken cancellationToken)
        {
            var foto = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.Id == request.FotoUrlId)
                .FirstOrDefaultAsync(cancellationToken);

            var fotoVm = _mapper.Map<FotoUrlVm>(foto);
            return fotoVm;
        }
    }
}
