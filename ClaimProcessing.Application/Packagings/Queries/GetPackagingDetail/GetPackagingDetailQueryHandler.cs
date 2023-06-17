using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingDetail
{
    public class GetPackagingDetailQueryHandler : IRequestHandler<GetPackagingDetailQuery, PackagingDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetPackagingDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<PackagingDetailVm> Handle(GetPackagingDetailQuery request, CancellationToken cancellationToken)
        {
            var packaging = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.Id == request.PackagingId)
                .FirstOrDefaultAsync(cancellationToken);

            var packagingVm = _mapper.Map<PackagingDetailVm>(packaging);
            return packagingVm;
        }
    }
}
