using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.FotoUrls.Queries.GetFotoUrlDetail
{
    public class GetFotoUrlDetailQueryHandler : IRequestHandler<GetFotoUrlDetailQuery, FotoUrlDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetFotoUrlDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<FotoUrlDetailVm> Handle(GetFotoUrlDetailQuery request, CancellationToken cancellationToken)
        {
            var foto = await _context.FotoUrls
                .Where(a => a.StatusId != 0 && a.Id == request.FotoUrlId)
                .FirstOrDefaultAsync(cancellationToken);

            var fotoVm = _mapper.Map<FotoUrlDetailVm>(foto);
            return fotoVm;
        }
    }
}
