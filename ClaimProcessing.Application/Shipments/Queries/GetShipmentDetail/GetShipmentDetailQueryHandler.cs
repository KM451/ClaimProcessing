using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Shipments.Queries.GetShipmentDetail
{
    public class GetShipmentDetailQueryHandler : IRequestHandler<GetShipmentDetailQuery, ShipmentDetailVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetShipmentDetailQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<ShipmentDetailVm> Handle(GetShipmentDetailQuery request, CancellationToken cancellationToken)
        {
            var shipment = await _context.Shipments
                .Where(s => s.StatusId != 0 && s.Id == request.ShipmentId)
                .FirstOrDefaultAsync(cancellationToken);

            var shipmentVm = _mapper.Map<ShipmentDetailVm>(shipment);

            return shipmentVm;
        }
    }
}
