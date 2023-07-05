using AutoMapper;
using ClaimProcessing.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimProcessing.Application.Suppliers.Queries.GetSupplierClaims
{
    public class GetSupplierCliamsQueryHandler : IRequestHandler<GetSupplierClaimsQuery, SupplierClaimsVm>
    {
        private readonly IClaimProcessingDbContext _context;
        private IMapper _mapper;
        public GetSupplierCliamsQueryHandler(IClaimProcessingDbContext claimProcessingDbContext, IMapper mapper)
        {
            _context = claimProcessingDbContext;
            _mapper = mapper;
        }
        public async Task<SupplierClaimsVm> Handle(GetSupplierClaimsQuery request, CancellationToken cancellationToken)
        {
            var supplierClaims = await _context.Claims
                .Include(p => p.Shipment)
                .Include(p => p.Supplier)
                .Where(p => p.StatusId != 0 && p.SupplierId == request.SupplierId)
                .ToListAsync(cancellationToken);

            var supplierClaimsVm = new SupplierClaimsVm
            {
                SupplierClaims = supplierClaims.Select(src => _mapper.Map<SupplierClaimsDto>(src)).ToList()
            };

            return supplierClaimsVm;
        }
    }
}
