using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentPackagings
{
    public class GetShipmentPackagingsQueryHandler : IRequestHandler<GetShipmentPackagingsQuery, ShipmentPackagingsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetShipmentPackagingsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ShipmentPackagingsVm> Handle(GetShipmentPackagingsQuery request, CancellationToken cancellationToken)
        {
            var packagings = await _context.Packagings
                .Where(p => p.StatusId != 0 && p.ShipmentId == request.ShipmentId)
                .ToListAsync(cancellationToken);

            var packagingsVm = new ShipmentPackagingsVm
            {
                Packagings = packagings.Select(src => _mapper.Map<ShipmentPackagingsDto>(src)).ToList()
            };

            return packagingsVm;
        }
    }
}
