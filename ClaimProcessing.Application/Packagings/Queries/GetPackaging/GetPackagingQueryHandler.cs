using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackaging
{
    public class GetPackagingQueryHandler : IRequestHandler<GetPackagingQuery, PackagingVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetPackagingQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<PackagingVm> Handle(GetPackagingQuery request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.Id == request.PackagingId)
                .FirstOrDefaultAsync(cancellationToken);

            var packagingVm = _mapper.Map<PackagingVm>(packaging);
            return packagingVm;
        }
    }
}
