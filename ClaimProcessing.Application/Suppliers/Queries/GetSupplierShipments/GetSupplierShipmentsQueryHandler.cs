using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierShipments
{
    public class GetSupplierShipmentsQueryHandler : IRequestHandler<GetSupplierShipmentsQuery, SupplierShipmentsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;

        public GetSupplierShipmentsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }

        public async Task<SupplierShipmentsVm> Handle(GetSupplierShipmentsQuery request, CancellationToken cancellationToken)
        {
            var supplierShipments = await _context.Shipments
                .Where(s => s.StatusId != 0 && s.SupplierId == request.SupplierId)
                .ToListAsync(cancellationToken);

            var supplierShipmentsVm = new SupplierShipmentsVm
            {
                SupplierShipments = supplierShipments.Select(src => _mapper.Map<SupplierShipmentsDto>(src)).ToList()
            };

            return supplierShipmentsVm;
        }
    }
}
