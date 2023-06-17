using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Packagings.Queries.GetPackagingsShipment
{
    public class GetPackagingsShipmentQueryHandler : IRequestHandler<GetPackagingsShipmentQuery, PackagingsShipmentVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetPackagingsShipmentQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<PackagingsShipmentVm> Handle(GetPackagingsShipmentQuery request, CancellationToken cancellationToken)
        {
            var packagings = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.ShipmentId== request.ShipmentId)
                .ToListAsync(cancellationToken);

            var packagingsVm = _mapper.Map<PackagingsShipmentVm>(packagings);
            
            return packagingsVm;
        }
    }
}
