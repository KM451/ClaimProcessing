using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentClaims
{
    public class GetShipmentClaimsQueryHandler : IRequestHandler<GetShipmentClaimsQuery, ShipmentClaimsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public GetShipmentClaimsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task<ShipmentClaimsVm> Handle(GetShipmentClaimsQuery request, CancellationToken cancellationToken)
        {
            var claimSupplier = await _context.Claims
                .Where(p => p.StatusId != 0 && p.ShipmentId == request.ShipmentId)
                .ToListAsync(cancellationToken);

            var claimShipmentVm = new ShipmentClaimsVm
            {
                ShipmentClaims = claimSupplier.Select(src => _mapper.Map<ShipmentClaimsDto>(src)).ToList()
            };
            return claimShipmentVm;
        }

    }
}
