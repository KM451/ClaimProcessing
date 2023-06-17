using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipments
{
    public class GetShipmentsQueryHandler : IRequestHandler<GetShipmentsQuery, ShipmentsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public GetShipmentsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ShipmentsVm> Handle(GetShipmentsQuery request, CancellationToken cancellationToken)
        {
            var shipments = await _context.Shipments
                .Where(p => p.StatusId != 0)
                .Include(c => c.Supplier)
                .ToListAsync(cancellationToken);

            var shipmentsVm = _mapper.Map<ShipmentsVm>(shipments);

            return shipmentsVm;
        }
    }
}
